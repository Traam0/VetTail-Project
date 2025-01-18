using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.DTOs.Generics;
using VetTail.Application.Common.DTOs.Pets;
using VetTail.Application.Common.DTOs.Wrappers;
using VetTail.Application.Common.Exceptions;
using VetTail.Application.Common.Interfaces;
using VetTail.Application.Common.Interfaces.repositories;
using VetTail.Domain.Common.Exceptions;
using VetTail.Domain.Common.Interfaces;
using VetTail.Domain.Entities;

namespace VetTail.Application.Services;

internal class PetsService : IPetService
{
    private readonly IPetRepository repository;
    private readonly IValidationFactory validationFactory;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public PetsService(IPetRepository repository, IValidationFactory validationFactory, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.repository = repository;
        this.validationFactory = validationFactory;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<PetDto> GetPetById(Guid id, CancellationToken cancellationToken = default)
    {
        Pet pet = await this.repository.FindByIdWithOwner(id, cancellationToken) 
            ?? throw EntityNullReferenceException.Build<Pet, Guid>(id);
        
        return this.mapper.Map<PetDto>(pet);
    }
    
    public async Task<PaginatedList<PetBriefDto>> GetAllPaginated(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        IEnumerable<PetBriefDto> records = (await this.repository.GetAllAsync(cancellationToken)).Select(this.mapper.Map<PetBriefDto>);

        return PaginatedList<PetBriefDto>.Create(
            records, pageNumber, pageSize);
    }

    public async Task<Result<Guid>> CreatePetAsync(CreatePetDto dto, CancellationToken cancellationToken = default)
    {
        IValidator<CreatePetDto>? validator = this.validationFactory.GetValidator<CreatePetDto>();

        if (validator != null) {
            ValidationResult result = await validator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid) throw new ValidationFailureException(result.Errors);
        }


        Pet pet = this.mapper.Map<Pet>(dto);
        pet = await this.repository.AddAsync(pet, cancellationToken);
        
        if(await this.unitOfWork.SaveChangeAsync(cancellationToken) == false)
        {
            return Result<Guid>.Failure("Failed to save changes to the database.");
        }

        return Result<Guid>.Success(pet.Id);
    }

    public async Task<Result<Guid>> UpdatePetAsync(UpdatePetDto dto, CancellationToken cancellationToken = default)
    {
        IValidator<UpdatePetDto>? validator = this.validationFactory.GetValidator<UpdatePetDto>();

        if (validator != null)
        {
            ValidationResult result = await validator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid) throw new ValidationFailureException(result.Errors);
        }

        Pet pet = this.mapper.Map<Pet>(dto);
        await repository.UpdateAsync(pet, cancellationToken);

        if(await this.unitOfWork.SaveChangeAsync(cancellationToken) == false)
        {
            return Result<Guid>.Failure("Failed to save changes to the database");
        }

        return Result<Guid>.Success(pet.Id);
    }

    public async Task<Result> DeletePetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Pet pet = await this.repository.FindByIdAsync(id, cancellationToken) ?? throw EntityNullReferenceException.Build<Pet, Guid>(id);
        await this.repository.DeleteAsync(pet, cancellationToken);

        if(await this.unitOfWork.SaveChangeAsync(cancellationToken) == false)
        {
            return Result.Failure("Failed fulfill the delete operation.");
        }

        return Result.Success();
    }
}

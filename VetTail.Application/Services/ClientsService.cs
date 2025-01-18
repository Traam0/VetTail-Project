using System;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Application.Common.Interfaces;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.DTOs.Wrappers;
using VetTail.Application.Common.Interfaces.repositories;
using AutoMapper;
using VetTail.Domain.Common.Interfaces;
using VetTail.Application.Common.DTOs.Generics;
using VetTail.Domain.Entities;
using VetTail.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace VetTail.Application.Services;

public class ClientsService : IClientService
{
    private readonly IClientRepository repository;
    private readonly IValidationFactory validationFactory;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public ClientsService(IClientRepository repository, IValidationFactory validationFactory, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.validationFactory = validationFactory;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ClientDto> GetClientByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Client record = await this.repository.FindByIdWithPets(id, cancellationToken)
                ?? throw EntityNullReferenceException.Build<Client, Guid>(id);

        return this.mapper.Map<ClientDto>(record);
    }

    public async Task<PaginatedList<ClientBriefDto>> GetAllPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        IEnumerable<ClientBriefDto> records = (await this.repository.GetAllAsync(cancellationToken)).Select(this.mapper.Map<ClientBriefDto>);

        return PaginatedList<ClientBriefDto>.Create(
            records,
            pageNumber,
            pageSize
        );
    }

    public async Task<Result<Guid>> CreateClientAsync(CreateClientDto dto, CancellationToken cancellationToken = default)
    {

        //IValidator<CreateClientDto>? validator = this.validationFactory.GetValidator<CreateClientDto>();
        //if(validator != null)
        //{
        //    ValidationResult result = await validator.ValidateAsync(dto, cancellationToken);
        //    if (!result.IsValid) throw new ValidationFailureException(result.Errors);
        //}

        Client client = this.mapper.Map<Client>(dto);
        Client record = await this.repository.AddAsync(client, cancellationToken);
        if(await this.unitOfWork.SaveChangeAsync(cancellationToken) == false)
        {
            return Result<Guid>.Failure("Failed to save changes to the database");
        }

        return Result<Guid>.Success(record.Id);
    }

    public async Task<Result<Guid>> UpdateClientAsync(UpdateClientDto dto, CancellationToken cancellationToken = default)
    {
        //IValidator<UpdateClientDto>? validator = this.validationFactory.GetValidator<UpdateClientDto>();

        //if (validator != null)
        //{
        //    ValidationResult result = await validator.ValidateAsync(dto, cancellationToken);
        //    if (!result.IsValid) throw new ValidationException(result.Errors);
        //}

        Client client = this.mapper.Map<Client>(dto);
        await this.repository.UpdateAsync(client, cancellationToken);
        if(await this.unitOfWork.SaveChangeAsync(cancellationToken) == false)
        {
            return Result<Guid>.Failure("Failed to save changes to the database");
        }
        return Result<Guid>.Success(client.Id);
    }

    public async Task<Result> DeleteClientAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Client client = await this.repository.FindByIdAsync(id, cancellationToken)
            ?? throw EntityNullReferenceException.Build<Client, Guid>(id);

        await this.repository.DeleteAsync(client, cancellationToken);

        if(await this.unitOfWork.SaveChangeAsync(cancellationToken) == false)
        {
            return Result.Failure();
        }

        return Result.Success();
    }

    public async Task<IEnumerable<ClientMacroDto>> GetAllClientsMacroAsync(CancellationToken cancellation = default)
    {
        return (await this.repository.GetAllAsync(cancellation)).Select(this.mapper.Map<ClientMacroDto>);
    }
}

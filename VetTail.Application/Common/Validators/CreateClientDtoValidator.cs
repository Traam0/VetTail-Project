using System;
using FluentValidation;
using VetTail.Application.Common.DTOs.Clients;

namespace VetTail.Application.Common.Validators;

public sealed class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
{
    public CreateClientDtoValidator()
    {
        RuleFor(dto => dto.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Matches("/^[a-zA-Z]$/").WithMessage("{PropertyName} must contain letters only.")
            .MinimumLength(3).WithMessage("{PropertyName}'s lenght must be >= 3");

        RuleFor(dto => dto.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Matches("/^[a-zA-Z]$/").WithMessage("{PropertyName} must contain letters only.")
            .MinimumLength(3).WithMessage("{PropertyName}'s lenght must be >= 3");


        RuleFor(dto => dto.MiddleName)
            .Cascade(CascadeMode.Stop)
            .Matches("/^[a-zA-Z]$/").WithMessage("{PropertyName} must contain letters only.")
            .MinimumLength(3).WithMessage("{PropertyName}'s lenght must be >= 3.")
            .When(dto => !string.IsNullOrEmpty(dto.MiddleName), ApplyConditionTo.AllValidators);

        RuleFor(dto => dto.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Matches("/^\\w+\\.?\\w+@\\w+.\\w{2,}$/").WithMessage("Illegal {PropertyName}'s format.")
            .When(dto => !string.IsNullOrEmpty(dto.Email), ApplyConditionTo.AllValidators);

        RuleFor(dto => dto.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Matches("^(\\+[1-9]{1,3}|0)(\\d{9})$").WithMessage("Illegal {PropertyName}'s format.");


        RuleFor(dto => dto.City)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MinimumLength(3).WithMessage("{PropertyName}'s lenght must be >= 3.");

        RuleFor(dto => dto.Street)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(dto => dto.BirthDate)
             .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is required,")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("{PropertyName} can't be in the future");

        RuleFor(dto => dto.Gender)
            .NotNull().WithMessage("{PropertyName} is required,");
    }
}

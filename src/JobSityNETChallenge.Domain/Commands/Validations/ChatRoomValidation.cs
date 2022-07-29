using FluentValidation;
using System;

namespace JobSityNETChallenge.Domain.Commands.Validations
{
    public abstract class ChatRoomValidation<T> : AbstractValidator<T> where T : ChatRoomCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters");
        }

        protected void ValidateCreatedOn()
        {
            RuleFor(c => c.CreatedOn)
                .NotEmpty()
                .WithMessage("CreatedOn is required");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
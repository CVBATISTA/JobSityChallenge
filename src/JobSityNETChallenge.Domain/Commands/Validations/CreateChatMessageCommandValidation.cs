using FluentValidation;
using JobSityNETChallenge.Domain.Models;

namespace JobSityNETChallenge.Domain.Commands.Validations
{
    public class CreateChatMessageCommandValidation : AbstractValidator<CreateChatMessageCommand>
    {
        public CreateChatMessageCommandValidation()
        {
            RuleFor(c => c.Text)
                .NotEmpty().WithMessage("Text can't be empty")
                .Length(1, 254).WithMessage("The Text must have between 1 and 254 characters");

            RuleFor(c => c.CreatedByUserId)
                .NotEqual(Guid.Empty);

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

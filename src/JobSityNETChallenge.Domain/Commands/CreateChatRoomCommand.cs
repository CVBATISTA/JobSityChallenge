using JobSityNETChallenge.Domain.Commands.Validations;

namespace JobSityNETChallenge.Domain.Commands
{
    public class CreateChatRoomCommand : ChatRoomCommand
    {
        public CreateChatRoomCommand(string name, Guid createdByUserId)
        {
            Name = name;
            CreatedByUserId = createdByUserId;
        }

        public Guid CreatedByUserId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateChatRoomCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
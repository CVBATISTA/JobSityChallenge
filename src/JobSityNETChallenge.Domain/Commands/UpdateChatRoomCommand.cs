using JobSityNETChallenge.Domain.Commands.Validations;

namespace JobSityNETChallenge.Domain.Commands
{
    public class UpdateChatRoomCommand : ChatRoomCommand
    {
        public UpdateChatRoomCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateChatRoomCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
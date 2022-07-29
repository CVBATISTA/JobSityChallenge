using System;
using JobSityNETChallenge.Domain.Commands.Validations;

namespace JobSityNETChallenge.Domain.Commands
{
    public class RemoveChatRoomCommand : ChatRoomCommand
    {
        public RemoveChatRoomCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveChatRoomCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

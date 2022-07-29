using System;
using JobSityNETChallenge.Domain.Commands.Validations;
using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Commands
{
    public class CreateChatMessageCommand : Command
    {
        public CreateChatMessageCommand(Guid createdByUserId, Guid chatRoomId, string userName, string text)
        {
            CreatedByUserId = createdByUserId;
            ChatRoomId = chatRoomId;
            UserName = userName;
            Text = text;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CreatedByUserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public string UserName { get; set; } = null!;
        public string Text { get; set; } = null!;

        public override bool IsValid()
        {
            ValidationResult = new CreateChatMessageCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

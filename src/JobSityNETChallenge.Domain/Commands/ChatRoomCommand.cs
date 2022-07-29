using System;
using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Commands
{
    public class ChatRoomCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}

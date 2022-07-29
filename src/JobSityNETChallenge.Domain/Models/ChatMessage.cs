using NetDevPack.Domain;
using Microsoft.AspNetCore.Identity;

namespace JobSityNETChallenge.Domain.Models
{
    public class ChatMessage : Entity, IAggregateRoot
    {
        public ChatMessage(Guid userId, Guid chatRoomId, string userName, string text)
        {
            UserId = userId;
            ChatRoomId = chatRoomId;
            UserName = userName;
            Text = text;
        }

        // Empty constructor for EF
        protected ChatMessage() { }

        public string Text { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
    }
}

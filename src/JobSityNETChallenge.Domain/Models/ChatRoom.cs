using NetDevPack.Domain;

namespace JobSityNETChallenge.Domain.Models
{
    public class ChatRoom : Entity, IAggregateRoot
    {
        public ChatRoom(Guid id, Guid createdByUserId, string name)
        {
            Name = name;
            CreatedByUserId = createdByUserId;
        }

        // Empty constructor for EF
        protected ChatRoom() { }

        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserId { get;set; }
        public virtual List<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}

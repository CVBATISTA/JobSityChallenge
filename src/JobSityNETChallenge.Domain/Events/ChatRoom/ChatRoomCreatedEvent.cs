using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Events.ChatRoom
{
    public class ChatRoomCreatedEvent : Event
    {
        public ChatRoomCreatedEvent(Guid id, Guid createdByUserId, string name, DateTime createdOn)
        {
            Id = id;
            CreatedByUserId = createdByUserId;
            Name = name;
            CreatedOn = createdOn;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Name { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
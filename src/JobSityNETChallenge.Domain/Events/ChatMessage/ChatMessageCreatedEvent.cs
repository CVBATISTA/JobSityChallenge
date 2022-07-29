using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Events.ChatMessage
{
    public class ChatMessageCreatedEvent : Event
    {
        public ChatMessageCreatedEvent(Guid id, Guid createdByUserId, string text, DateTime createdOn)
        {
            Id = id;
            CreatedByUserId = createdByUserId;
            Text = text;
            CreatedOn = createdOn;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Text { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
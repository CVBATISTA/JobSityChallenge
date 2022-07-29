using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Events.ChatRoom
{
    public class ChatRoomUpdatedEvent : Event
    {
        public ChatRoomUpdatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }
    }
}
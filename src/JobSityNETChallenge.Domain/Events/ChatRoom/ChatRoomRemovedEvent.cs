using System;
using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Events.ChatRoom
{
    public class ChatRoomRemovedEvent : Event
    {
        public ChatRoomRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
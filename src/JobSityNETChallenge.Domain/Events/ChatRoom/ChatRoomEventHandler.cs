using MediatR;

namespace JobSityNETChallenge.Domain.Events.ChatRoom
{
    public class ChatRoomEventHandler :
        INotificationHandler<ChatRoomCreatedEvent>
    {
        public Task Handle(ChatRoomCreatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
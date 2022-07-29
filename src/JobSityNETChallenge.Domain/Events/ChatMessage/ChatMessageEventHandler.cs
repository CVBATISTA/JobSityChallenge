using MediatR;

namespace JobSityNETChallenge.Domain.Events.ChatMessage
{
    public class ChatMessageEventHandler :
        INotificationHandler<ChatMessageCreatedEvent>
    {
        public Task Handle(ChatMessageCreatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
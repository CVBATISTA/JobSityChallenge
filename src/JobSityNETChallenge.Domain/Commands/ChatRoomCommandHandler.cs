using JobSityNETChallenge.Domain.Interfaces;
using JobSityNETChallenge.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using JobSityNETChallenge.Domain.Events.ChatRoom;
using JobSityNETChallenge.Domain.Events.ChatMessage;

namespace JobSityNETChallenge.Domain.Commands
{
    public class ChatRoomCommandHandler : CommandHandler,
        IRequestHandler<CreateChatRoomCommand, ValidationResult>,
        IRequestHandler<UpdateChatRoomCommand, ValidationResult>,
        IRequestHandler<RemoveChatRoomCommand, ValidationResult>,
        IRequestHandler<CreateChatMessageCommand, ValidationResult>
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatRoomCommandHandler(IChatRoomRepository chatRoomRepository, IChatMessageRepository chatMessageRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<ValidationResult> Handle(CreateChatRoomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var chatRoom = new ChatRoom(Guid.NewGuid(), message.CreatedByUserId, message.Name);

            chatRoom.AddDomainEvent(new ChatRoomCreatedEvent(chatRoom.Id, message.CreatedByUserId, message.Name, chatRoom.CreatedOn));

            _chatRoomRepository.Add(chatRoom);

            return await Commit(_chatRoomRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateChatRoomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingChatRoom = await _chatRoomRepository.GetById(message.Id);

            if (existingChatRoom is null)
            {
                AddError("The chat room doesn't exists.");
                return ValidationResult;
            }

            existingChatRoom.Name = message.Name;

            existingChatRoom.AddDomainEvent(new ChatRoomUpdatedEvent(existingChatRoom.Id, existingChatRoom.Name));

            _chatRoomRepository.Update(existingChatRoom);

            return await Commit(_chatRoomRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveChatRoomCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var chatRoom = await _chatRoomRepository.GetById(message.Id);

            if (chatRoom is null)
            {
                AddError("The chatRoom doesn't exists.");
                return ValidationResult;
            }

            chatRoom.AddDomainEvent(new ChatRoomRemovedEvent(message.Id));

            _chatRoomRepository.Remove(chatRoom);

            return await Commit(_chatRoomRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _chatRoomRepository.Dispose();
        }

        public async Task<ValidationResult> Handle(CreateChatMessageCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var chatMessage = new ChatMessage(message.CreatedByUserId, message.ChatRoomId, message.UserName, message.Text);

            chatMessage.AddDomainEvent(new ChatMessageCreatedEvent(chatMessage.Id, message.CreatedByUserId, message.Text, chatMessage.CreatedOn));

            _chatMessageRepository.Add(chatMessage);

            return await Commit(_chatMessageRepository.UnitOfWork);
        }
    }
}
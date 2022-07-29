using AutoMapper;
using JobSityNETChallenge.Application.Interfaces;
using JobSityNETChallenge.Application.ViewModels;
using JobSityNETChallenge.Domain.Commands;
using JobSityNETChallenge.Domain.Interfaces;
using JobSityNETChallenge.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;
using NetDevPack.Mediator;
using NetDevPack.Identity.User;
using Microsoft.AspNetCore.Http;

namespace JobSityNETChallenge.Application.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _customerRepository;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IMapper _mapper;

        public ChatRoomService(IMapper mapper,
                                  IChatRoomRepository customerRepository,
                                  IMediatorHandler mediator,
                                  IAspNetUser user,
                                  IChatMessageRepository chatMessageRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _mediator = mediator;
            _user = user;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<IEnumerable<ChatRoomViewModel>> GetAll()
        {
            var x = await _customerRepository.GetAll();
            return _mapper.Map<IEnumerable<ChatRoomViewModel>>(x.OrderBy(p => p.CreatedOn));
        }

        public async Task<ChatRoomViewModel> GetById(Guid id)
        {
            await CreateMessage(new ChatMessageViewModel { Text = "teste", ChatRoomId = id });
            return _mapper.Map<ChatRoomViewModel>(await _customerRepository.GetById(id));
        }

        public async Task<ValidationResult> Create(ChatRoomViewModel customerViewModel)
        {
            customerViewModel.CreatedByUserId = _user.GetUserId();
            var registerCommand = _mapper.Map<CreateChatRoomCommand>(customerViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(ChatRoomViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateChatRoomCommand>(customerViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveChatRoomCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> CreateMessage(ChatMessageViewModel chatMessageViewModel)
        {
            var registerCommand = _mapper.Map<CreateChatMessageCommand>(chatMessageViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<ChatMessageViewModel>> GetMessages(Guid id, DateTime? latest)
        {
            return latest == null ?
                _mapper.Map<IEnumerable<ChatMessageViewModel>>(_chatMessageRepository.GetAllByChatRoomId(id).Result.OrderByDescending(x => x.CreatedOn).Take(50))
                : _mapper.Map<IEnumerable<ChatMessageViewModel>>(_chatMessageRepository.GetAllByChatRoomId(id).Result.Where(x => x.CreatedOn > latest));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

using AutoMapper;
using JobSityNETChallenge.Application.ViewModels;
using JobSityNETChallenge.Domain.Commands;

namespace JobSityNETChallenge.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ChatRoomViewModel, CreateChatRoomCommand>()
                .ConstructUsing(c => new CreateChatRoomCommand(c.Name, c.CreatedByUserId));

            CreateMap<ChatRoomViewModel, UpdateChatRoomCommand>()
                .ConstructUsing(c => new UpdateChatRoomCommand(c.Id, c.Name));

            CreateMap<ChatMessageViewModel, CreateChatMessageCommand>()
                .ConstructUsing(c => new CreateChatMessageCommand(c.CreatedByUserId, c.ChatRoomId, c.CreatedByUserName, c.Text));
        }
    }
}

using AutoMapper;
using JobSityNETChallenge.Application.ViewModels;
using JobSityNETChallenge.Domain.Models;

namespace JobSityNETChallenge.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ChatRoom, ChatRoomViewModel>();
            CreateMap<ChatMessage, ChatMessageViewModel>()
                .ConstructUsing(e => new ChatMessageViewModel { CreatedByUserName = e.UserName, CreatedByUserId = e.UserId, Text = e.Text, CreatedOn = e.CreatedOn });
        }
    }
}

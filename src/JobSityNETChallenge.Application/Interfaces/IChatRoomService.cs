using JobSityNETChallenge.Application.ViewModels;
using FluentValidation.Results;

namespace JobSityNETChallenge.Application.Interfaces
{
    public interface IChatRoomService : IDisposable
    {
        Task<IEnumerable<ChatRoomViewModel>> GetAll();
        Task<ChatRoomViewModel> GetById(Guid id);

        Task<ValidationResult> Create(ChatRoomViewModel customerViewModel);
        Task<ValidationResult> Update(ChatRoomViewModel customerViewModel);
        Task<ValidationResult> Remove(Guid id);
        Task<IEnumerable<ChatMessageViewModel>> GetMessages(Guid id, DateTime? latest);
        Task<ValidationResult> CreateMessage(ChatMessageViewModel chatMessageViewModel);
    }
}

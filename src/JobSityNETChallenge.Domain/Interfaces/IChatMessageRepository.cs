using JobSityNETChallenge.Domain.Models;
using NetDevPack.Data;

namespace JobSityNETChallenge.Domain.Interfaces
{
    public interface IChatMessageRepository : IRepository<ChatMessage>
    {
        Task<ChatMessage> GetById(Guid id);
        Task<IEnumerable<ChatMessage>> GetAll();
        Task<IEnumerable<ChatMessage>> GetAllByChatRoomId(Guid chatRoomId);

        void Add(ChatMessage customer);
        void Update(ChatMessage customer);
        void Remove(ChatMessage customer);
    }
}
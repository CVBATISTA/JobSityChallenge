using JobSityNETChallenge.Domain.Models;
using NetDevPack.Data;

namespace JobSityNETChallenge.Domain.Interfaces
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        Task<ChatRoom> GetById(Guid id);
        Task<IEnumerable<ChatRoom>> GetAll();

        void Add(ChatRoom customer);
        void Update(ChatRoom customer);
        void Remove(ChatRoom customer);
    }
}
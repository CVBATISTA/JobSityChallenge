using JobSityNETChallenge.Domain.Interfaces;
using JobSityNETChallenge.Domain.Models;
using JobSityNETChallenge.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace JobSityNETChallenge.Infra.Data.Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        protected readonly JobSityNETChallengeContext Db;
        protected readonly DbSet<ChatMessage> DbSet;

        public ChatMessageRepository(JobSityNETChallengeContext context)
        {
            Db = context;
            DbSet = Db.Set<ChatMessage>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<ChatMessage> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<ChatMessage>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(ChatMessage customer)
        {
            DbSet.Add(customer);
        }

        public void Update(ChatMessage customer)
        {
            DbSet.Update(customer);
        }

        public void Remove(ChatMessage customer)
        {
            DbSet.Remove(customer);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<IEnumerable<ChatMessage>> GetAllByChatRoomId(Guid chatRoomId)
        {
            return await DbSet.Where(p => p.ChatRoomId == chatRoomId).ToListAsync();
        }
    }
}

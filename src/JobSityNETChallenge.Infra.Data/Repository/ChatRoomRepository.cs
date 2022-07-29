using JobSityNETChallenge.Domain.Interfaces;
using JobSityNETChallenge.Domain.Models;
using JobSityNETChallenge.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;


namespace JobSityNETChallenge.Infra.Data.Repository
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        protected readonly JobSityNETChallengeContext Db;
        protected readonly DbSet<ChatRoom> DbSet;

        public ChatRoomRepository(JobSityNETChallengeContext context)
        {
            Db = context;
            DbSet = Db.Set<ChatRoom>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<ChatRoom> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<ChatRoom>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(ChatRoom customer)
        {
            DbSet.Add(customer);
        }

        public void Update(ChatRoom customer)
        {
            DbSet.Update(customer);
        }

        public void Remove(ChatRoom customer)
        {
            DbSet.Remove(customer);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}

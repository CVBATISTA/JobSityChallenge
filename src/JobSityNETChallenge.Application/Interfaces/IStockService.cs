using JobSityNETChallenge.Domain.Core.Models;

namespace JobSityNETChallenge.Application.Interfaces
{
    public interface IStockService
    {
        Task<bool> GetStock(string stockCode, Guid chatRoomId);
    }
}

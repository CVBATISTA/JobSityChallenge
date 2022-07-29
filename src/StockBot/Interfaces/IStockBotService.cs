using JobSityNETChallenge.Domain.Core.Models;

namespace StockBot.Interfaces
{
    public interface IStockBotService
    {
        public Stock GetStock(string stockCode);
    }
}

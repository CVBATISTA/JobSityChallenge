using JobSityNETChallenge.Application.Interfaces;
using JobSityNETChallenge.Application.ViewModels;
using JobSityNETChallenge.Domain.Core.Models;
using Newtonsoft.Json;

namespace JobSityNETChallenge.Application.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;
        private readonly IChatRoomService _chatRoomService;
        public StockService(HttpClient client, IChatRoomService chatRoomService)
        {
            _client = client;
            _chatRoomService = chatRoomService;
        }
        public async Task<bool> GetStock(string stockCode, Guid chatRoomId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync($"https://localhost:7010/StockBot/Stock?stock_code={stockCode}").Result;
                HttpContent content = response.Content;
                string botResponse = content.ReadAsStringAsync().Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(botResponse);

                var stock = JsonConvert.DeserializeObject<Stock>(botResponse);

                var message = new ChatMessageViewModel 
                {
                    ChatRoomId = chatRoomId,
                    Text = $"{stockCode} quote is ${stock.Close/100} per share",
                    CreatedByUserId = Guid.Parse("be7cb93f-83b7-4497-8aa7-03d106d0428b"),
                    CreatedByUserName = "stockbot@bot.com"
                };
                await _chatRoomService.CreateMessage(message);
                return true;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

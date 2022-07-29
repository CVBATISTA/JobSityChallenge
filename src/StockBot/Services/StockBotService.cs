using JobSityNETChallenge.Domain.Core.Models;
using StockBot.Interfaces;

namespace StockBot.Services
{
    public class StockBotService : IStockBotService
    {
        private readonly HttpClient _client;
        public StockBotService(HttpClient client)
        {
            _client = client;
        }

        public Stock GetStock(string stockCode)
        {
            HttpResponseMessage response = _client.GetAsync($"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv").Result;
            HttpContent content = response.Content;
            var result = content.ReadAsStringAsync().Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new ArgumentException(result);
            var data = result.Substring(result.IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
            var processedArray = data.Split(',');
            var stock = new Stock()
            {
                Symbol = processedArray[0],
                Date = !processedArray[1].Contains("N/D") ? Convert.ToDateTime(processedArray[1]) : default,
                Time = !processedArray[2].Contains("N/D") ? Convert.ToDateTime(processedArray[2]).TimeOfDay : default,
                Open = !processedArray[3].Contains("N/D") ? Convert.ToDouble(processedArray[3]) : default,
                High = !processedArray[4].Contains("N/D") ? Convert.ToDouble(processedArray[4]) : default,
                Low = !processedArray[5].Contains("N/D") ? Convert.ToDouble(processedArray[5]) : default,
                Close = !processedArray[6].Contains("N/D") ? Convert.ToDouble(processedArray[6]) : default,
                Volume = !processedArray[7].Contains("N/D") ? Convert.ToDouble(processedArray[7]) : default,
            };
            if (stock.Date.Equals(DateTime.MinValue))
                throw new Exception($"Stock '{stockCode}' not found");
            return stock;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using StockBot.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class StockBotTests
    {
        [Fact(DisplayName = "Get valid stock from the StockBot")]
        [Trait("Category", "StockBot")]
        public async Task GetStockSuccess()
        {
            var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {});

            var client = application.CreateClient();
            var response = await client.GetAsync("https://localhost:7010/StockBot/Stock?stock_code=AAC-U.US");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact(DisplayName = "Get invalid stock from the StockBot")]
        [Trait("Category", "StockBot")]
        public async Task GetStockFailure()
        {
            var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => { });

            var client = application.CreateClient();
            var response = await client.GetAsync("https://localhost:7010/StockBot/Stock?stock_code=SSSSSSSSS");
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}

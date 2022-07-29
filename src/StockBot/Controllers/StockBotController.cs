using JobSityNETChallenge.Domain.Core.Models;
using Microsoft.AspNetCore.Mvc;
using StockBot.Interfaces;

namespace StockBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockBotController : Controller
    {
        private readonly IStockBotService _stockBotService;
        public StockBotController(IStockBotService stockBotService)
        {
            _stockBotService = stockBotService;
        }

        [HttpGet]
        [Route("Stock")]
        public ActionResult<Stock> GetStock([FromQuery] string stock_code)
        {
            try
            {
                var result = _stockBotService.GetStock(stock_code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

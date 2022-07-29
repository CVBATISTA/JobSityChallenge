using JobSityNETChallenge.Application.Interfaces;
using JobSityNETChallenge.Application.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NetDevPack.Identity.User;

namespace JobSityNETChallenge.Services.Api.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatRoomHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IAspNetUser _user;
        private readonly IStockService _stockService;

        public ChatRoomHub(IChatRoomService chatRoomService, IAspNetUser user, IStockService stockService)
        {
            _chatRoomService = chatRoomService;
            _user = user;
            _stockService = stockService;
        }

        public async Task SendMessage(Guid chatRoomId, string text)
        {
            var message = new ChatMessageViewModel { ChatRoomId = chatRoomId, Text = text, CreatedByUserId = _user.GetUserId(), CreatedByUserName = _user.GetUserEmail() };
            await _chatRoomService.CreateMessage(message);
            await Clients.All.SendAsync("ReceiveMessage", new { chatRoomId =  chatRoomId });
            if (text.ToLower().Contains("/stock="))
            {
                try
                {
                    var stockResult = await _stockService.GetStock(text.ToLower().Split("/stock=").Last(), chatRoomId);
                    if(stockResult)
                        await Clients.All.SendAsync("ReceiveMessage", new { chatRoomId = chatRoomId });

                }
                catch (Exception e)
                {
                    var botErrorMessage = new ChatMessageViewModel { ChatRoomId = chatRoomId, Text = e.Message, CreatedByUserId = Guid.Parse("be7cb93f-83b7-4497-8aa7-03d106d0428b"), CreatedByUserName = "stockbot@bot.com" };
                    await _chatRoomService.CreateMessage(botErrorMessage);
                    await Clients.All.SendAsync("ReceiveMessage", new { chatRoomId = chatRoomId });
                    throw;
                }
            }

        }

        public async Task Teste()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}

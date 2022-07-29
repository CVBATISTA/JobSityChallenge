using JobSityNETChallenge.Application.Interfaces;
using JobSityNETChallenge.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;


namespace JobSityNETChallenge.Services.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatRoomController : ApiController
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }
        [HttpGet("api/chat-room-management")]
        public async Task<IEnumerable<ChatRoomViewModel>> Get()
        {
            return await _chatRoomService.GetAll();
        }

        [HttpGet("api/chat-room/{id}/chat-messages")]
        public async Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(Guid id, [FromQuery] DateTime? lastMessageTime)
        {
            var response  = await _chatRoomService.GetMessages(id, lastMessageTime);
            return response;
        }

        [HttpGet("api/chat-room-management/{id:guid}")]
        public async Task<ChatRoomViewModel> Get(Guid id)
        {
            return await _chatRoomService.GetById(id);
        }

        [HttpPost("api/chat-room-management")]
        public async Task<IActionResult> Post([FromBody] ChatRoomViewModel customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _chatRoomService.Create(customerViewModel));
        }

        [HttpPut("api/chat-room-management")]
        public async Task<IActionResult> Put([FromBody] ChatRoomViewModel customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _chatRoomService.Update(customerViewModel));
        }

        [HttpDelete("api/chat-room-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _chatRoomService.Remove(id));
        }
    }
}

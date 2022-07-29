namespace JobSityNETChallenge.Application.ViewModels
{
    public class ChatMessageViewModel
    {
        public Guid CreatedByUserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedByUserName { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}

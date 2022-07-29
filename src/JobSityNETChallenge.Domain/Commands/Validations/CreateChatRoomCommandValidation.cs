namespace JobSityNETChallenge.Domain.Commands.Validations
{
    public class CreateChatRoomCommandValidation : ChatRoomValidation<CreateChatRoomCommand>
    {
        public CreateChatRoomCommandValidation()
        {
            ValidateName();
        }
    }
}
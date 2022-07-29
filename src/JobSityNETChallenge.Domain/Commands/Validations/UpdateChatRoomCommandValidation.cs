namespace JobSityNETChallenge.Domain.Commands.Validations
{
    public class UpdateChatRoomCommandValidation : ChatRoomValidation<UpdateChatRoomCommand>
    {
        public UpdateChatRoomCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
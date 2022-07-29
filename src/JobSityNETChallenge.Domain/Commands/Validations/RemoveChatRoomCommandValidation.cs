using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityNETChallenge.Domain.Commands.Validations
{
    public class RemoveChatRoomCommandValidation : ChatRoomValidation<RemoveChatRoomCommand>
    {
        public RemoveChatRoomCommandValidation()
        {
            ValidateId();
        }
    }
}
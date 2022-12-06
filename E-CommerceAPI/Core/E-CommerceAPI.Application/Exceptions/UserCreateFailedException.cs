using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class UserCreateFailedException: Exception
    {
        public UserCreateFailedException(): base ("There is some error during User Creating Process")
        {

        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(SerializationInfo info, StreamingContext context): base (info, context)
        {

        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

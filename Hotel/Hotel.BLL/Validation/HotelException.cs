using System;
using System.Runtime.Serialization;

namespace Hotel.BLL.Validation
{
    [Serializable]
    public class HotelException : Exception
    {
        public string Property { get; protected set; }

        public HotelException()
        {
        }

        public HotelException(string message) : base(message)
        {
        }

        public HotelException(string message, string prop) : base(message)
        {
            Property = prop;
        }

        protected HotelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

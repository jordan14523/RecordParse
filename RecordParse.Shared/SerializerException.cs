using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RecordParse.Shared
{
    public class SerializerException : Exception
    {
        public SerializerException(string message) : base(message)
        {
        }

        public SerializerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SerializerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

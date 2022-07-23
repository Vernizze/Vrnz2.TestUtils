using System;
using System.Runtime.Serialization;

namespace Vrnz2.TestUtils.Exceptions
{
    [Serializable]
    public class EnumValueNotFound
        : Exception
    {
        public EnumValueNotFound()
        {
        }

        public EnumValueNotFound(string message)
            : base(message)
        {
        }

        public EnumValueNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected EnumValueNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

using System;
using System.Runtime.Serialization;

namespace SoftwareDesign.GTM.Helper
{
    [Serializable]
    public class InvalidTemplateException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidTemplateException()
        {
        }

        public InvalidTemplateException(string message) : base(message)
        {
        }

        public InvalidTemplateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidTemplateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
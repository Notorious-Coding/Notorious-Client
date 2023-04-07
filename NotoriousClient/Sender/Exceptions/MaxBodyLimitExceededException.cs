using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NotoriousClient.Sender.Exceptions
{
    public class MaxBodyLimitExceededException : Exception
    {
        public HttpRequestMessage request { get; set; }

        public MaxBodyLimitExceededException(string? message, HttpRequestMessage request) : base(message)
        {
        }

    }
}

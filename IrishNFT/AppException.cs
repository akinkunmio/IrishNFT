using Microsoft.AspNetCore.Http;
using System.Net;

namespace IrishNFT
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException(string message)
           : base(message)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
        public AppException(string message, int statusCode)
          : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(string message, string friendlyMessage)
            : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }
        public string FriendlyMessage { get; set; } = string.Empty;

        public int? StatusCode { get; set; }

        public object Value { get; set; } = string.Empty;
    }
}

using System.Text;
using Newtonsoft.Json;

namespace Gremlin.Net.Driver
{
    internal class JsonMessageSerializer
    {
        private const string MimeType = "application/vnd.gremlin-v2.0+json";

        public byte[] SerializeMessage(string msg)
        {
            return Encoding.UTF8.GetBytes(MessageWithHeader(msg));
        }

        private string MessageWithHeader(string messageContent)
        {
            return $"{(char)MimeType.Length}{MimeType}{messageContent}";
        }

        public TMessage DeserializeMessage<TMessage>(byte[] message)
        {
            var responseStr = Encoding.UTF8.GetString(message);
            return JsonConvert.DeserializeObject<TMessage>(responseStr);
        }
    }
}
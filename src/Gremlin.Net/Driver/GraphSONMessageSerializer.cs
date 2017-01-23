using System.Collections.Generic;
using System.Text;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Driver
{
    public class GraphSONMessageSerializer
    {
        private const string MimeType = "application/vnd.gremlin-v2.0+json";
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new BytecodeConverter(),
                new Int32Converter(),
                new Int64Converter(),
                new FloatConverter(),
                new DoubleConverter()
            }
        };

        public byte[] SerializeMessage(RequestMessage message)
        {
            var payload = JsonConvert.SerializeObject(message, _serializerSettings);
            var messageWithHeader = $"{(char)MimeType.Length}{MimeType}{payload}";
            return Encoding.UTF8.GetBytes(messageWithHeader);
        }

        public TMessage DeserializeMessage<TMessage>(byte[] message)
        {
            var responseStr = Encoding.UTF8.GetString(message);
            return JsonConvert.DeserializeObject<TMessage>(responseStr);
        }
    }
}
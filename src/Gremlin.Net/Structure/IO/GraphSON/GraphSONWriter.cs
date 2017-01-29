using System.Collections.Generic;
using System.Text;
using Gremlin.Net.Driver.Messages;
using Newtonsoft.Json;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class GraphSONWriter
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
    }
}
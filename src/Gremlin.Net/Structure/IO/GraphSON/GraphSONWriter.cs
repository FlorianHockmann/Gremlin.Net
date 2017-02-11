using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Driver.Messages.Traversal;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    public class GraphSONWriter
    {
        private const string MimeType = "application/vnd.gremlin-v2.0+json";

        private readonly Dictionary<Type, IGraphSONSerializer> _serializerByGraphSONType = new Dictionary
            <Type, IGraphSONSerializer>
            {
                {typeof(Traversal), new TraversalSerializer()},
                {typeof(Bytecode), new BytecodeSerializer()},
                {typeof(BytecodeRequestMessage), new BytecodeRequestMessageSerializer()},
                {typeof(int), new Int32Converter()},
                {typeof(long), new Int64Converter()},
                {typeof(float), new FloatConverter()},
                {typeof(double), new DoubleConverter()}
            };

        public byte[] SerializeMessage(RequestMessage message)
        {
            var payload = WriteObject(message);
            var messageWithHeader = $"{(char)MimeType.Length}{MimeType}{payload}";
            return Encoding.UTF8.GetBytes(messageWithHeader);
        }

        public string WriteObject(dynamic objectData)
        {
            return JsonConvert.SerializeObject(ToDict(objectData));
        }

        internal dynamic ToDict(dynamic objectData)
        {
            var type = objectData.GetType();
            IGraphSONSerializer serializer;
            if(TryGetSerializerFor(out serializer, type))
            {
                return serializer.Dictify(objectData, this);
            }
            return objectData;
        }

        private bool TryGetSerializerFor(out IGraphSONSerializer serializer, Type type)
        {
            if (_serializerByGraphSONType.ContainsKey(type))
            {
                serializer = _serializerByGraphSONType[type];
                return true;
            }
            foreach (var supportedType in _serializerByGraphSONType.Keys)
            {
                if (supportedType.IsAssignableFrom(type))
                {
                    serializer = _serializerByGraphSONType[supportedType];
                    return true;
                }
            }
            serializer = null;
            return false;
        }
    }
}
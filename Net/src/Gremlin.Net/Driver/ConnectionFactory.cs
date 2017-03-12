using System;
using Gremlin.Net.Structure.IO.GraphSON;

namespace Gremlin.Net.Driver
{
    internal class ConnectionFactory
    {
        private readonly GraphSONReader _graphSONReader;
        private readonly GraphSONWriter _graphSONWriter;
        private readonly Uri _uri;

        public ConnectionFactory(Uri uri, GraphSONReader graphSONReader, GraphSONWriter graphSONWriter)
        {
            _uri = uri;
            _graphSONReader = graphSONReader;
            _graphSONWriter = graphSONWriter;
        }

        public Connection CreateConnection()
        {
            return new Connection(_uri, _graphSONReader, _graphSONWriter);
        }
    }
}
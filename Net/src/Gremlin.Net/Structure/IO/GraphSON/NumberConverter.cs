using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal abstract class NumberConverter : IGraphSONDeserializer, IGraphSONSerializer
    {
        protected abstract string GraphSONTypeName { get; }
        protected abstract Type HandledType { get; }

        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            return graphsonObject.ToObject(HandledType);
        }

        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            return GraphSONUtil.ToTypedValue(GraphSONTypeName, objectData);
        }
    }
}
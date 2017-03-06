using System;
using System.Collections.Generic;

namespace Gremlin.Net.Driver.Messages
{
    public class RequestMessage
    {
        /// <summary>
        /// Gets the ID of this request message.
        /// </summary>
        /// <value>A UUID representing the unique identification for the request.</value>
        public Guid RequestId { get; }

        /// <summary>
        /// Gets or sets the name of the operation that should be executed by the Gremlin Server.
        /// </summary>
        /// <value>The name of the "operation" to execute based on the available OpProcessor configured in the Gremlin Server. This defaults to "eval" which evaluates a request script.</value>
        public string Operation { get; }

        /// <summary>
        /// Gets or sets the name of the OpProcessor to utilize.
        /// </summary>
        /// <value>The name of the OpProcessor to utilize. This defaults to an empty string which represents the default OpProcessor for evaluating scripts.</value>
        public string Processor { get; }

        public Dictionary<string, object> Arguments { get; }

        public RequestMessage(Guid requestId, string operation, string processor, Dictionary<string, object> arguments)
        {
            RequestId = requestId;
            Operation = operation;
            Processor = processor;
            Arguments = arguments;
        }

        public static RequestMessageBuilder Build(string operation)
        {
            return new RequestMessageBuilder(operation);
        }
    }

    public class RequestMessageBuilder
    {
        private static string DefaultProcessor = "";
        private Guid _requestId = Guid.NewGuid();
        private readonly string _operation;
        private string _processor = DefaultProcessor;
        private readonly Dictionary<string, object> _arguments = new Dictionary<string, object>();

        public RequestMessageBuilder(string operation)
        {
            _operation = operation;
        }

        public RequestMessageBuilder Processor(string processor)
        {
            _processor = processor;
            return this;
        }

        public RequestMessageBuilder OverrideRequestId(Guid requestId)
        {
            _requestId = requestId;
            return this;
        }

        public RequestMessageBuilder AddArgument(string key, object value)
        {
            _arguments.Add(key, value);
            return this;
        }

        public RequestMessage Create()
        {
            return new RequestMessage(_requestId, _operation, _processor, _arguments);
        }
    }
}
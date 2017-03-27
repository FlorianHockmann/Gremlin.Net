using System;
using System.Collections.Generic;

namespace Gremlin.Net.Driver.Messages
{
    /// <summary>
    ///     The model for a request message sent to the server.
    /// </summary>
    public class RequestMessage
    {
        private RequestMessage(Guid requestId, string operation, string processor, Dictionary<string, object> arguments)
        {
            RequestId = requestId;
            Operation = operation;
            Processor = processor;
            Arguments = arguments;
        }

        /// <summary>
        ///     Gets the ID of this request message.
        /// </summary>
        /// <value>A UUID representing the unique identification for the request.</value>
        public Guid RequestId { get; }

        /// <summary>
        ///     Gets the name of the operation that should be executed by the Gremlin Server.
        /// </summary>
        /// <value>
        ///     The name of the "operation" to execute based on the available OpProcessor configured in the Gremlin Server. This
        ///     defaults to "eval" which evaluates a request script.
        /// </value>
        public string Operation { get; }

        /// <summary>
        ///     Gets the name of the OpProcessor to utilize.
        /// </summary>
        /// <value>
        ///     The name of the OpProcessor to utilize. This defaults to an empty string which represents the default
        ///     OpProcessor for evaluating scripts.
        /// </value>
        public string Processor { get; }

        /// <summary>
        ///     Gets arguments of the <see cref="RequestMessage" />.
        /// </summary>
        public Dictionary<string, object> Arguments { get; }

        /// <summary>
        ///     Initializes a <see cref="Builder" /> to build a <see cref="RequestMessage" />.
        /// </summary>
        /// <param name="operation">The name of the OpProcessor to utilize.</param>
        /// <returns>A <see cref="Builder" /> to build a <see cref="RequestMessage" />.</returns>
        public static Builder Build(string operation)
        {
            return new Builder(operation);
        }

        /// <summary>
        ///     Allows to build <see cref="RequestMessage" /> objects.
        /// </summary>
        public class Builder
        {
            private const string DefaultProcessor = "";
            private readonly Dictionary<string, object> _arguments = new Dictionary<string, object>();
            private readonly string _operation;
            private string _processor = DefaultProcessor;
            private Guid _requestId = Guid.NewGuid();

            internal Builder(string operation)
            {
                _operation = operation;
            }

            /// <summary>
            ///     If this value is not set in the builder then the <see cref="RequestMessage.Processor" /> defaults to
            ///     the standard op processor (empty string).
            /// </summary>
            /// <param name="processor">The name of the processor.</param>
            /// <returns>The <see cref="Builder" />.</returns>
            public Builder Processor(string processor)
            {
                _processor = processor;
                return this;
            }

            /// <summary>
            ///     Overrides the request identifier with a specified one, otherwise the
            ///     <see cref="Builder" /> will randomly generate a <see cref="Guid" />.
            /// </summary>
            /// <param name="requestId">The request identifier to use.</param>
            /// <returns>The <see cref="Builder" />.</returns>
            public Builder OverrideRequestId(Guid requestId)
            {
                _requestId = requestId;
                return this;
            }

            /// <summary>
            ///     Adds and argument to the <see cref="RequestMessage" />.
            /// </summary>
            /// <param name="key">The key of the argument.</param>
            /// <param name="value">The value of the argument.</param>
            /// <returns>The <see cref="Builder" />.</returns>
            public Builder AddArgument(string key, object value)
            {
                _arguments.Add(key, value);
                return this;
            }

            /// <summary>
            ///     Creates the <see cref="RequestMessage" /> given the settings provided to the <see cref="Builder" />.
            /// </summary>
            /// <returns>The built <see cref="RequestMessage" />.</returns>
            public RequestMessage Create()
            {
                return new RequestMessage(_requestId, _operation, _processor, _arguments);
            }
        }
    }
}
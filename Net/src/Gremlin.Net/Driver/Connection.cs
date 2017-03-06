#region License
/*
 * Copyright 2016 Florian Hockmann
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Driver
{
    internal class Connection : IConnection
    {
        private readonly Uri _uri;
        private readonly GraphSONWriter _graphSONWriter;
        private readonly GraphSONReader _graphSONReader;
        private readonly WebSocketConnection _webSocketConnection = new WebSocketConnection();

        public Connection(Uri uri, GraphSONReader graphSONReader, GraphSONWriter graphSONWriter)
        {
            _uri = uri;
            _graphSONReader = graphSONReader;
            _graphSONWriter = graphSONWriter;
        }

        public async Task ConnectAsync()
        {
            await _webSocketConnection.ConnectAsync(_uri).ConfigureAwait(false);
        }

        public async Task CloseAsync()
        {
            await _webSocketConnection.CloseAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> SubmitAsync<T>(RequestMessage requestMessage)
        {
            await SendAsync(requestMessage).ConfigureAwait(false);
            return await ReceiveAsync<T>().ConfigureAwait(false);
        }

        private async Task SendAsync(RequestMessage message)
        {
            var serializedMsg = _graphSONWriter.SerializeMessage(message);
            await _webSocketConnection.SendMessageAsync(serializedMsg).ConfigureAwait(false);
        }

        private async Task<IEnumerable<T>> ReceiveAsync<T>()
        {
            ResponseStatus status;
            IAggregator aggregator = null;
            var isAggregatingSideEffects = false;
            var result = new List<T>();
            do
            {
                var received = await _webSocketConnection.ReceiveMessageAsync().ConfigureAwait(false);
                var responseStr = Encoding.UTF8.GetString(received);
                var receivedMsg = JObject.Parse(responseStr);

                status = JsonConvert.DeserializeObject<ResponseStatus>(receivedMsg["status"].ToString());
                status.ThrowIfStatusIndicatesError();

                if (status.Code != ResponseStatusCode.NoContent)
                {
                    var receivedData = _graphSONReader.ToObject(receivedMsg["result"]["data"]);
                    foreach (var d in receivedData)
                    {
                        if (receivedMsg["result"]["meta"]["sideEffectKey"] != null)
                        {
                            if (aggregator == null)
                            {
                                aggregator =
                                    new AggregatorFactory().GetAggregatorFor(
                                        (string) receivedMsg["result"]["meta"]["aggregateTo"]);
                            }
                            aggregator.Add(d);
                            isAggregatingSideEffects = true;
                        }
                        else
                        {
                            result.Add(d);
                        }
                    }
                }
            } while (status.Code == ResponseStatusCode.PartialContent);

            if (isAggregatingSideEffects)
            {
                return new List<T> {(T) aggregator.GetAggregatedResult()};
            }
            return result;
        }

        #region IDisposable Support
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _webSocketConnection?.Dispose();
                }
                _disposed = true;
            }
        }
        #endregion
    }

    internal class AggregatorFactory
    {
        public IAggregator GetAggregatorFor(string aggregateTo)
        {
            if (aggregateTo == "map")
            {
                return new DictionaryAggregator();
            }
            if (aggregateTo == "bulkset")
            {
                return new TraverserAggregator();
            }
            return new DefaultAggregator();
        }
    }

    internal class DefaultAggregator : IAggregator
    {
        private readonly List<dynamic> _result = new List<dynamic>();

        public void Add(object value)
        {
            _result.Add(value);
        }

        public object GetAggregatedResult()
        {
            return _result;
        }
    }

    internal class DictionaryAggregator : IAggregator
    {
        private readonly Dictionary<string, dynamic> _result = new Dictionary<string, dynamic>();

        public void Add(object value)
        {
            var newEntry = ((Dictionary<string, dynamic>) value).First();
            _result.Add(newEntry.Key, newEntry.Value);
        }

        public object GetAggregatedResult()
        {
            return _result;
        }
    }

    internal class TraverserAggregator : IAggregator
    {
        private readonly Dictionary<object, long> _result = new Dictionary<object, long>();

        public void Add(object value)
        {
            var traverser = (Traverser) value;
            _result.Add(traverser.Object, traverser.Bulk);
        }

        public object GetAggregatedResult()
        {
            return _result;
        }
    }

    internal interface IAggregator
    {
        void Add(object value);
        object GetAggregatedResult();
    }
}
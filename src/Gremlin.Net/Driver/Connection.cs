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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net.Driver.Messages;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Driver
{
    internal class Connection : IConnection
    {
        private readonly GraphSONWriter _graphSONWriter = new GraphSONWriter();
        private readonly GraphSONReader _graphSONReader = new GraphSONReader();
        private readonly WebSocketConnection _webSocketConnection = new WebSocketConnection();
        
        public async Task ConnectAsync(Uri uri)
        {
            await _webSocketConnection.ConnectAsync(uri).ConfigureAwait(false);
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
                    foreach(var d in receivedData)
                        result.Add(d);
                }
            } while (status.Code == ResponseStatusCode.PartialContent);

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
}
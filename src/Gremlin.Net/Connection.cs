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
using System.Threading.Tasks;
using Gremlin.Net.Messages;

namespace Gremlin.Net
{
    internal class Connection : IConnection
    {
        private readonly JsonMessageSerializer _messageSerializer = new JsonMessageSerializer();
        private readonly WebSocketConnection _webSocketConnection = new WebSocketConnection();
        
        public async Task ConnectAsync(Uri uri)
        {
            await _webSocketConnection.ConnectAsync(uri).ConfigureAwait(false);
        }

        public async Task CloseAsync()
        {
            await _webSocketConnection.CloseAsync().ConfigureAwait(false);
        }

        public async Task<IList<T>> SubmitAsync<T>(ScriptRequestMessage requestMessage)
        {
            await SendAsync(requestMessage).ConfigureAwait(false);
            return await ReceiveAsync<T>().ConfigureAwait(false);
        }

        private async Task SendAsync(ScriptRequestMessage message)
        {
            var serializedMsg = _messageSerializer.SerializeMessage(message);
            await _webSocketConnection.SendMessageAsync(serializedMsg).ConfigureAwait(false);
        }

        private async Task<IList<T>> ReceiveAsync<T>()
        {
            ResponseStatus status;
            var result = new List<T>();
            do
            {
                var received = await _webSocketConnection.ReceiveMessageAsync().ConfigureAwait(false);
                var receivedMessage = _messageSerializer.DeserializeMessage<ResponseMessage<T>>(received);

                status = receivedMessage.Status;
                status.ThrowIfStatusIndicatesError();

                if (status.Code != ResponseStatusCode.NoContent)
                    result.AddRange(receivedMessage.Result.Data);
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
﻿#region License
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
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Gremlin.NetCore
{
    internal class WebSocketConnection : IDisposable
    {
        private const int ReceiveBufferSize = 1024;
        private const WebSocketMessageType MessageType = WebSocketMessageType.Binary;
        private ClientWebSocket _client;
        
        public async Task ConnectAsync(Uri uri)
        {
            _client = new ClientWebSocket();
            await _client.ConnectAsync(uri, CancellationToken.None);
        }

        public async Task CloseAsync()
        {
            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        }

        public async Task SendMessageAsync(byte[] message)
        {
            await _client.SendAsync(new ArraySegment<byte>(message), MessageType, true, CancellationToken.None);
        }

        public async Task<byte[]> ReceiveMessageAsync()
        {
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult received;
                do
                {
                    var receiveBuffer = new ArraySegment<byte>(new byte[ReceiveBufferSize]);
                    received = await _client.ReceiveAsync(receiveBuffer, CancellationToken.None);
                    ms.Write(receiveBuffer.Array, receiveBuffer.Offset, received.Count);
                } while (!received.EndOfMessage);

                return ms.ToArray();
            }
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
                    _client?.Dispose();
                }
                _disposed = true;
            }
        }
        #endregion
    }
}
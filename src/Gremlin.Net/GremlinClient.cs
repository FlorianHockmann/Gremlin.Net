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
    /// <summary>
    /// Provides a mechanism for submitting Gremlin requests to one Gremlin Server.
    /// </summary>
    public class GremlinClient : IGremlinClient
    {
        private readonly ConnectionPool _connectionPool;

        /// <summary>
        /// Initializes a new instance of the <see cref="GremlinClient"/> class for the specified Gremlin Server.
        /// </summary>
        /// <param name="gremlinServer">The <see cref="GremlinServer"/> the requests should be sent to.</param>
        public GremlinClient(GremlinServer gremlinServer)
        {
            _connectionPool = new ConnectionPool(gremlinServer.Uri, gremlinServer.Username, gremlinServer.Password);
        }

        /// <summary>
        /// Gets the number of open connections.
        /// </summary>
        public int NrConnections => _connectionPool.NrConnections;

        /// <inheritdoc />
        public async Task<IEnumerable<T>> SubmitAsync<T>(ScriptRequestMessage requestMessage)
        {
            using (var connection = await _connectionPool.GetAvailableConnectionAsync().ConfigureAwait(false))
                return await connection.SubmitAsync<T>(requestMessage).ConfigureAwait(false);
        }

        #region IDisposable Support
        private bool _disposed;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources used by the <see cref="GremlinClient"/> instance.
        /// </summary>
        /// <param name="disposing">Specifies whether managed resources should be released.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connectionPool?.Dispose();
                }
                _disposed = true;
            }
        }
        #endregion
    }
}
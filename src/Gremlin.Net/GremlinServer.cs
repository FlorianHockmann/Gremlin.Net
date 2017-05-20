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

namespace Gremlin.Net
{
    /// <summary>
    /// Represents a Gremlin Server.
    /// </summary>
    public class GremlinServer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GremlinServer"/> class with the specified connection parameters.
        /// </summary>
        /// <param name="hostname">The hostname of the server.</param>
        /// <param name="port">The port on which Gremlin Server can be reached.</param>
        /// <param name="enableSsl">Specifies whether SSL should be enabled.</param>
        /// <param name="Username">The username to connect the Gremlin Server.</param>
        /// <param name="Password">The password to connect the Gremlin Server.</param>
        public GremlinServer(string hostname, int port = 8182, bool enableSsl = false, string Username = null, string Password = null)
        {
            Uri = CreateUri(hostname, port, enableSsl);
            this.Username = Username;
            this.Password = Password;
        }

        /// <summary>
        /// Gets the URI of the Gremlin Server.
        /// </summary>
        /// <value>The WebSocket <see cref="System.Uri"/> that the Gremlin Server responds to.</value>
        public Uri Uri { get; }

        /// <summary>
        /// Gets the username to connect the Gremlin Server.
        /// </summary>
        /// <value>The username to connect the Gremlin Server.</value>
        public string Username { get; }

        /// <summary>
        /// Gets the password to connect the Gremlin Server.
        /// </summary>
        /// <value>The password to connect the Gremlin Server.</value>
        public string Password { get; }

        private Uri CreateUri(string hostname, int port, bool enableSsl)
        {
            var scheme = enableSsl ? "wss" : "ws";
            return new Uri($"{scheme}://{hostname}:{port}/gremlin");
        }
    }
}
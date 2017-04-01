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

using System.Threading.Tasks;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.Process.Remote
{
    /// <summary>
    ///     A simple abstraction of a "connection" to a "server".
    /// </summary>
    public interface IRemoteConnection
    {
        /// <summary>
        ///     Submits <see cref="Traversal.Traversal" /> <see cref="Bytecode" /> to a server and returns a
        ///     <see cref="Traversal.Traversal" />.
        /// </summary>
        /// <param name="bytecode">The <see cref="Bytecode" /> to send.</param>
        /// <returns>The <see cref="Traversal.Traversal" /> with the results and optional side-effects.</returns>
        Task<Traversal.Traversal> SubmitAsync(Bytecode bytecode);
    }
}
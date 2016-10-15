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

using System.Collections.Generic;
using Gremlin.NetCore.Messages;

namespace Gremlin.NetCore.IntegrationTest.Util
{
    internal class ScriptRequestMessageProvider
    {
        public ScriptRequestMessage GetSleepMessage(int sleepTimeInMs)
        {
            var gremlinScript = $"Thread.sleep({nameof(sleepTimeInMs)});";
            var bindings = new Dictionary<string, object> {{nameof(sleepTimeInMs), sleepTimeInMs}};
            return new ScriptRequestMessage
            {
                Arguments =
                    new ScriptRequestArguments
                    {
                        GremlinScript = gremlinScript,
                        Bindings = bindings
                    }
            };
        }

        public ScriptRequestMessage GetDummyMessage()
        {
            var gremlinScript = "1";
            return new ScriptRequestMessage {Arguments = new ScriptRequestArguments {GremlinScript = gremlinScript}};
        }
    }
}
/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Gremlin.Net.Process;

namespace Gremlin.CSharp.Process
{
    public class GraphTraversalSource
    {
        public Bytecode Bytecode { get; set; }

        public GraphTraversalSource()
            : this(new Bytecode())
        {
        }

        public GraphTraversalSource(Bytecode bytecode)
        {
            Bytecode = bytecode;
        }

        public GraphTraversalSource WithBulk(params object[] args)
        {
            var source = new GraphTraversalSource(Bytecode);
            source.Bytecode.AddSource("withBulk", args);
            return source;
        }

        public GraphTraversalSource WithPath(params object[] args)
        {
            var source = new GraphTraversalSource(Bytecode);
            source.Bytecode.AddSource("withPath", args);
            return source;
        }

        public GraphTraversalSource WithSack(params object[] args)
        {
            var source = new GraphTraversalSource(Bytecode);
            source.Bytecode.AddSource("withSack", args);
            return source;
        }

        public GraphTraversalSource WithSideEffect(params object[] args)
        {
            var source = new GraphTraversalSource(Bytecode);
            source.Bytecode.AddSource("withSideEffect", args);
            return source;
        }

        public GraphTraversalSource WithStrategies(params object[] args)
        {
            var source = new GraphTraversalSource(Bytecode);
            source.Bytecode.AddSource("withStrategies", args);
            return source;
        }

        public GraphTraversalSource WithoutStrategies(params object[] args)
        {
            var source = new GraphTraversalSource(Bytecode);
            source.Bytecode.AddSource("withoutStrategies", args);
            return source;
        }

        public GraphTraversalSource WithBindings(object bindings)
        {
            return this;
        }

        public GraphTraversal E(params object[] args)
        {
            var traversal = new GraphTraversal(Bytecode);
            traversal.Bytecode.AddStep("E", args);
            return traversal;
        }

        public GraphTraversal V(params object[] args)
        {
            var traversal = new GraphTraversal(Bytecode);
            traversal.Bytecode.AddStep("V", args);
            return traversal;
        }

        public GraphTraversal AddV(params object[] args)
        {
            var traversal = new GraphTraversal(Bytecode);
            traversal.Bytecode.AddStep("addV", args);
            return traversal;
        }

        public GraphTraversal Inject(params object[] args)
        {
            var traversal = new GraphTraversal(Bytecode);
            traversal.Bytecode.AddStep("inject", args);
            return traversal;
        }
	}
}

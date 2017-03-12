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
using System.Threading.Tasks;

namespace Gremlin.Net.Process.Traversal
{
    public abstract class Traversal : IDisposable, IEnumerator
    {
        private IEnumerator<Traverser> _traverserEnumerator;
        public Bytecode Bytecode { get; protected set; }
        public ITraversalSideEffects SideEffects { get; set; }
        public IEnumerable<Traverser> Traversers { get; set; }
        protected IList<ITraversalStrategy> TraversalStrategies { get; set; } = new List<ITraversalStrategy>();

        private IEnumerator<Traverser> TraverserEnumerator
            => _traverserEnumerator ?? (_traverserEnumerator = GetTraverserEnumerator());

        public void Dispose()
        {
            Dispose(true);
        }

        public bool MoveNext()
        {
            var currentTraverser = TraverserEnumerator.Current;
            if (currentTraverser?.Bulk > 1)
            {
                currentTraverser.Bulk--;
                return true;
            }
            return TraverserEnumerator.MoveNext();
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public object Current => TraverserEnumerator.Current?.Object;

        private IEnumerator<Traverser> GetTraverserEnumerator()
        {
            if (Traversers == null)
                ApplyStrategies();
            return Traversers.GetEnumerator();
        }

        private void ApplyStrategies()
        {
            foreach (var strategy in TraversalStrategies)
                strategy.Apply(this);
        }

        private async Task ApplyStrategiesAsync()
        {
            foreach (var strategy in TraversalStrategies)
                await strategy.ApplyAsync(this).ConfigureAwait(false);
        }

        public object Next()
        {
            MoveNext();
            return Current;
        }

        public IEnumerable<object> Next(int amount)
        {
            for (var i = 0; i < amount; i++)
                yield return Next();
        }

        public Traversal Iterate()
        {
            while (MoveNext())
            {
            }
            return this;
        }

        public Traverser NextTraverser()
        {
            TraverserEnumerator.MoveNext();
            return TraverserEnumerator.Current;
        }

        public List<object> ToList()
        {
            var objs = new List<object>();
            while (MoveNext())
                objs.Add(Current);
            return objs;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                SideEffects?.Dispose();
        }

        public async Task<TReturn> Promise<TReturn>(Func<Traversal, TReturn> callback)
        {
            await ApplyStrategiesAsync().ConfigureAwait(false);
            return callback(this);
        }
    }
}
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

namespace Gremlin.Net.Process.Traversal
{
    public class Traverser
    {
        public Traverser(dynamic obj, long bulk = 1)
        {
            Object = obj;
            Bulk = bulk;
        }

        public dynamic Object { get; }
        public long Bulk { get; internal set; }

        public bool Equals(Traverser other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Object, other.Object);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Traverser)obj);
        }

        public override int GetHashCode()
        {
            return (Object != null ? Object.GetHashCode() : 0);
        }
    }
}
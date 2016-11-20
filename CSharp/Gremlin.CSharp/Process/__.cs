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
    public static class __
    {

        public static GraphTraversal V(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).V(args);
        }

        public static GraphTraversal AddE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).AddE(args);
        }

        public static GraphTraversal AddInE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).AddInE(args);
        }

        public static GraphTraversal AddOutE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).AddOutE(args);
        }

        public static GraphTraversal AddV(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).AddV(args);
        }

        public static GraphTraversal Aggregate(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Aggregate(args);
        }

        public static GraphTraversal And(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).And(args);
        }

        public static GraphTraversal As(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).As(args);
        }

        public static GraphTraversal Barrier(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Barrier(args);
        }

        public static GraphTraversal Both(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Both(args);
        }

        public static GraphTraversal BothE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).BothE(args);
        }

        public static GraphTraversal BothV(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).BothV(args);
        }

        public static GraphTraversal Branch(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Branch(args);
        }

        public static GraphTraversal Cap(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Cap(args);
        }

        public static GraphTraversal Choose(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Choose(args);
        }

        public static GraphTraversal Coalesce(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Coalesce(args);
        }

        public static GraphTraversal Coin(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Coin(args);
        }

        public static GraphTraversal Constant(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Constant(args);
        }

        public static GraphTraversal Count(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Count(args);
        }

        public static GraphTraversal CyclicPath(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).CyclicPath(args);
        }

        public static GraphTraversal Dedup(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Dedup(args);
        }

        public static GraphTraversal Drop(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Drop(args);
        }

        public static GraphTraversal Emit(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Emit(args);
        }

        public static GraphTraversal Filter(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Filter(args);
        }

        public static GraphTraversal FlatMap(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).FlatMap(args);
        }

        public static GraphTraversal Fold(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Fold(args);
        }

        public static GraphTraversal Group(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Group(args);
        }

        public static GraphTraversal GroupCount(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).GroupCount(args);
        }

        public static GraphTraversal GroupV3d0(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).GroupV3d0(args);
        }

        public static GraphTraversal Has(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Has(args);
        }

        public static GraphTraversal HasId(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).HasId(args);
        }

        public static GraphTraversal HasKey(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).HasKey(args);
        }

        public static GraphTraversal HasLabel(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).HasLabel(args);
        }

        public static GraphTraversal HasNot(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).HasNot(args);
        }

        public static GraphTraversal HasValue(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).HasValue(args);
        }

        public static GraphTraversal Id(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Id(args);
        }

        public static GraphTraversal Identity(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Identity(args);
        }

        public static GraphTraversal In(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).In(args);
        }

        public static GraphTraversal InE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).InE(args);
        }

        public static GraphTraversal InV(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).InV(args);
        }

        public static GraphTraversal Inject(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Inject(args);
        }

        public static GraphTraversal Is(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Is(args);
        }

        public static GraphTraversal Key(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Key(args);
        }

        public static GraphTraversal Label(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Label(args);
        }

        public static GraphTraversal Limit(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Limit(args);
        }

        public static GraphTraversal Local(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Local(args);
        }

        public static GraphTraversal Loops(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Loops(args);
        }

        public static GraphTraversal Map(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Map(args);
        }

        public static GraphTraversal MapKeys(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).MapKeys(args);
        }

        public static GraphTraversal MapValues(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).MapValues(args);
        }

        public static GraphTraversal Match(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Match(args);
        }

        public static GraphTraversal Max(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Max(args);
        }

        public static GraphTraversal Mean(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Mean(args);
        }

        public static GraphTraversal Min(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Min(args);
        }

        public static GraphTraversal Not(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Not(args);
        }

        public static GraphTraversal Optional(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Optional(args);
        }

        public static GraphTraversal Or(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Or(args);
        }

        public static GraphTraversal Order(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Order(args);
        }

        public static GraphTraversal OtherV(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).OtherV(args);
        }

        public static GraphTraversal Out(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Out(args);
        }

        public static GraphTraversal OutE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).OutE(args);
        }

        public static GraphTraversal OutV(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).OutV(args);
        }

        public static GraphTraversal Path(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Path(args);
        }

        public static GraphTraversal Project(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Project(args);
        }

        public static GraphTraversal Properties(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Properties(args);
        }

        public static GraphTraversal Property(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Property(args);
        }

        public static GraphTraversal PropertyMap(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).PropertyMap(args);
        }

        public static GraphTraversal Range(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Range(args);
        }

        public static GraphTraversal Repeat(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Repeat(args);
        }

        public static GraphTraversal Sack(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Sack(args);
        }

        public static GraphTraversal Sample(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Sample(args);
        }

        public static GraphTraversal Select(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Select(args);
        }

        public static GraphTraversal SideEffect(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).SideEffect(args);
        }

        public static GraphTraversal SimplePath(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).SimplePath(args);
        }

        public static GraphTraversal Store(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Store(args);
        }

        public static GraphTraversal Subgraph(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Subgraph(args);
        }

        public static GraphTraversal Sum(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Sum(args);
        }

        public static GraphTraversal Tail(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Tail(args);
        }

        public static GraphTraversal TimeLimit(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).TimeLimit(args);
        }

        public static GraphTraversal Times(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Times(args);
        }

        public static GraphTraversal To(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).To(args);
        }

        public static GraphTraversal ToE(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).ToE(args);
        }

        public static GraphTraversal ToV(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).ToV(args);
        }

        public static GraphTraversal Tree(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Tree(args);
        }

        public static GraphTraversal Unfold(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Unfold(args);
        }

        public static GraphTraversal Union(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Union(args);
        }

        public static GraphTraversal Until(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Until(args);
        }

        public static GraphTraversal Value(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Value(args);
        }

        public static GraphTraversal ValueMap(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).ValueMap(args);
        }

        public static GraphTraversal Values(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Values(args);
        }

        public static GraphTraversal Where(params object[] args)
        {
            return new GraphTraversal(new Bytecode()).Where(args);
        }
	}
}

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

namespace Gremlin.Net.Process.Traversal
{
    public class Bytecode
    {
        public List<Instruction> SourceInstructions { get; } = new List<Instruction>();
        public List<Instruction> StepInstructions { get; } = new List<Instruction>();

        public void AddSource(string sourceName, params object[] args)
        {
            SourceInstructions.Add(new Instruction(sourceName, args));
        }

        public void AddStep(string stepName, params object[] args)
        {
            StepInstructions.Add(new Instruction(stepName, args));
        }
    }
}
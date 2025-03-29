// Copyright 2014-2024 CaptiveAire Systems
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Concurrent;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog
{
    public class SerilogVariableContextEnricher : ILogEventEnricher
    {
        public static readonly ConcurrentVariableContext GlobalVariablesContext = new();

        public static readonly ThreadLocal<ConcurrentVariableContext> ThreadLocal =
            new(() => new ConcurrentVariableContext());

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            try
            {
                // enrich with global variables first...
                foreach (var globalVar in GlobalVariablesContext.GetAll())
                    logEvent.AddPropertyIfAbsent(
                        propertyFactory.CreateProperty(globalVar.Key, globalVar.Value, true));

                // then enrich with the thread variables
                foreach (var threadVar in ThreadLocal.Value.GetAll())
                    logEvent.AddPropertyIfAbsent(
                        propertyFactory.CreateProperty(threadVar.Key, threadVar.Value, true));
            }
            catch
            {
                // unfortunate situation, but not worth handling
            }
        }

        public class ConcurrentVariableContext : IVariablesContext
        {
            private readonly ConcurrentDictionary<string, object> _variables = new();

            public void Set(string key, object value)
            {
                _variables[key] = value;
            }

            public object? Get(string key)
            {
                return _variables.TryGetValue(key, out var value) ? value : null;
            }

            public bool Contains(string key)
            {
                return _variables.ContainsKey(key);
            }

            public void Remove(string key)
            {
                _variables.TryRemove(key, out _);
            }

            public void Clear()
            {
                _variables.Clear();
            }

            public IReadOnlyList<KeyValuePair<string, object>> GetAll()
            {
                return _variables.ToArray();
            }
        }
    }
}
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog
{
    public class SerilogVariableContextEnricher : ILogEventEnricher
    {
        public static readonly ConcurrentVariableContext GlobalVariablesContext = new ConcurrentVariableContext();

        public static readonly ThreadLocal<ConcurrentVariableContext> ThreadLocal =
            new ThreadLocal<ConcurrentVariableContext>(() => new ConcurrentVariableContext());

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            try
            {
                // enrich with global variables first...
                foreach (var globalVar in GlobalVariablesContext.GetAll())
                {
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(globalVar.Key, globalVar.Value, true));
                }

                // then enrich with the thread varables
                foreach (var threadVar in ThreadLocal.Value.GetAll())
                {
                    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(threadVar.Key, threadVar.Value, true));
                }
            }
            catch
            {
                // unfortunate situation, but not worth handling
            }
        }

        public class ConcurrentVariableContext : IVariablesContext
        {
            readonly ConcurrentDictionary<string, object> _variables = new ConcurrentDictionary<string, object>();

            public IReadOnlyList<KeyValuePair<string, object>> GetAll()
            {
                return this._variables.ToArray();
            }

            public void Set(string key, object value)
            {
                this._variables[key] = value;
            }

            public object Get(string key)
            {
                object value;
                return this._variables.TryGetValue(key, out value) ? value : null;
            }

            public bool Contains(string key)
            {
                return this._variables.ContainsKey(key);
            }

            public void Remove(string key)
            {
                object dummy;
                this._variables.TryRemove(key, out dummy);
            }

            public void Clear()
            {
                this._variables.Clear();
            }
        }
    }
}
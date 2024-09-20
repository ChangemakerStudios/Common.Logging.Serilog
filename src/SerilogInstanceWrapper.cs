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


using System.Diagnostics;

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog
{
    internal class SerilogInstanceWrapper : ILogger
    {
        private ILogger? _logger;

        private readonly Func<ILogger, ILogger> _configureContext;

        public SerilogInstanceWrapper(Func<ILogger, ILogger> configureContext, ILogger? specificInstance = null)
        {
            _configureContext = configureContext;
            if (specificInstance != null)
            {
                _logger = configureContext(specificInstance);
            }
        }

        private ILogger Logger
        {
            get
            {
                if (_logger == null && !Log.Logger.ToString().EndsWith("SilentLogger"))
                {
                    // if we actually have a global logger -- let's configure and use it.
                    _logger = _configureContext(Log.Logger);
                }

                if (_logger == null)
                {
                    Trace.WriteLine("Serilog Instance Not Available -- Logging is Silent", "Common.Logging.Serilog");
                }

                // will return the silent logger if it's null -- maybe it should fail?
                return _logger ?? Log.Logger;
            }
        }

        public ILogger ForContext(ILogEventEnricher enricher)
        {
            return Logger.ForContext(enricher);
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return Logger.ForContext(enrichers);
        }

        public ILogger ForContext(string propertyName, object? value, bool destructureObjects = false)
        {
            return Logger.ForContext(propertyName, value, destructureObjects);
        }

        public ILogger ForContext<TSource>()
        {
            return Logger.ForContext<TSource>();
        }

        public ILogger ForContext(Type source)
        {
            return Logger.ForContext(source);
        }

        public void Write(LogEvent logEvent)
        {
            Logger.Write(logEvent);
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
            Logger.Write(level, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            Logger.Write(level, messageTemplate, propertyValue);
        }

        public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Write(level, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Write(LogEventLevel level, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Write(level, messageTemplate, propertyValues);
        }

        public void Write(LogEventLevel level, Exception? exception, string messageTemplate)
        {
            Logger.Write(level, exception, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Write(level, exception, messageTemplate, propertyValue);
        }

        public void Write<T0, T1>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            Logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Write<T0, T1, T2>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Write(LogEventLevel level, Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Write(level, exception, messageTemplate, propertyValues);
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return Logger.IsEnabled(level);
        }

        public void Verbose(string messageTemplate)
        {
            Logger.Verbose(messageTemplate);
        }

        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
            Logger.Verbose(messageTemplate, propertyValue);
        }

        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Verbose(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Verbose(string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Verbose(messageTemplate, propertyValues);
        }

        public void Verbose(Exception? exception, string messageTemplate)
        {
            Logger.Verbose(exception, messageTemplate);
        }

        public void Verbose<T>(Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Verbose(exception, messageTemplate, propertyValue);
        }

        public void Verbose<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Verbose<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate)
        {
            Logger.Debug(messageTemplate);
        }

        public void Debug<T>(string messageTemplate, T propertyValue)
        {
            Logger.Debug(messageTemplate, propertyValue);
        }

        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Debug(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Debug(string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception? exception, string messageTemplate)
        {
            Logger.Debug(exception, messageTemplate);
        }

        public void Debug<T>(Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Debug(exception, messageTemplate, propertyValue);
        }

        public void Debug<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Debug<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Debug(exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate)
        {
            Logger.Information(messageTemplate);
        }

        public void Information<T>(string messageTemplate, T propertyValue)
        {
            Logger.Information(messageTemplate, propertyValue);
        }

        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Information(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Information(string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception? exception, string messageTemplate)
        {
            Logger.Information(exception, messageTemplate);
        }

        public void Information<T>(Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Information(exception, messageTemplate, propertyValue);
        }

        public void Information<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Information<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Information(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate)
        {
            Logger.Warning(messageTemplate);
        }

        public void Warning<T>(string messageTemplate, T propertyValue)
        {
            Logger.Warning(messageTemplate, propertyValue);
        }

        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Warning(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Warning(string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception? exception, string messageTemplate)
        {
            Logger.Warning(exception, messageTemplate);
        }

        public void Warning<T>(Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Warning(exception, messageTemplate, propertyValue);
        }

        public void Warning<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Warning<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate)
        {
            Logger.Error(messageTemplate);
        }

        public void Error<T>(string messageTemplate, T propertyValue)
        {
            Logger.Error(messageTemplate, propertyValue);
        }

        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Error(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error(string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception? exception, string messageTemplate)
        {
            Logger.Error(exception, messageTemplate);
        }

        public void Error<T>(Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Error(exception, messageTemplate, propertyValue);
        }

        public void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Error(exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate)
        {
            Logger.Fatal(messageTemplate);
        }

        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
            Logger.Fatal(messageTemplate, propertyValue);
        }

        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Fatal(messageTemplate, propertyValue0, propertyValue1);
        }

        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Fatal(string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception? exception, string messageTemplate)
        {
            Logger.Fatal(exception, messageTemplate);
        }

        public void Fatal<T>(Exception? exception, string messageTemplate, T propertyValue)
        {
            Logger.Fatal(exception, messageTemplate, propertyValue);
        }

        public void Fatal<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        public void Fatal<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            Logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues)
        {
            Logger.Fatal(exception, messageTemplate, propertyValues);
        }

        public bool BindMessageTemplate(string messageTemplate, object?[]? propertyValues, out MessageTemplate parsedTemplate,
            out IEnumerable<LogEventProperty> boundProperties)
        {
            return Logger.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate!, out boundProperties!);
        }

        public bool BindProperty(string? propertyName, object? value, bool destructureObjects, out LogEventProperty property)
        {
            return Logger.BindProperty(propertyName, value, destructureObjects, out property!);
        }
    }
}
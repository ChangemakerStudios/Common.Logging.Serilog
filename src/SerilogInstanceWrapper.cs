// Common.Logging.Serilog - Copyright (c) 2015 CaptiveAire

using System;
using System.Collections.Generic;
using System.Diagnostics;

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logging.Serilog
{
    internal class SerilogInstanceWrapper : ILogger
    {
        private ILogger _logger;

        private readonly Func<ILogger, ILogger> _configureContext;

        public SerilogInstanceWrapper(Func<ILogger, ILogger> configureContext, ILogger specificInstance = null)
        {
            _configureContext = configureContext;
            if (specificInstance != null)
            {
                _logger = configureContext(specificInstance);
            }
        }

        public ILogger Logger
        {
            get
            {
                if (_logger == null && Log.Logger != null && !Log.Logger.ToString().EndsWith("SilentLogger"))
                {
                    // if we actually have a global logger -- let's configure and use it.
                    _logger = _configureContext(_logger);
                }
                
                if (_logger == null)
                {
                    Trace.WriteLine("Serilog Instance Not Available -- Logging is Silent", "Common.Logging.Serilog");
                }

                // will return the silent logger if it's null -- maybe it should fail?
                return _logger ?? Log.Logger;
            }
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return Logger.ForContext(enrichers);
        }

        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
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

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, messageTemplate, propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, exception, messageTemplate, propertyValues);
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return Logger.IsEnabled(level);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}
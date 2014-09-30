// Copyright 2014 CaptiveAire Systems
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

namespace Common.Logging.Serilog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Serilog;

    /// <summary>
    ///     Serilog common logger.
    /// </summary>
    public class SerilogCommonLogger : ILog
    {
        readonly ILogger _logger;

        public SerilogCommonLogger(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Debug.ToSerilogEventLevel()); }
        }

        public bool IsErrorEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Error.ToSerilogEventLevel()); }
        }

        public bool IsFatalEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Fatal.ToSerilogEventLevel()); }
        }

        public bool IsInfoEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Info.ToSerilogEventLevel()); }
        }

        public bool IsTraceEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Trace.ToSerilogEventLevel()); }
        }

        public bool IsWarnEnabled
        {
            get { return _logger.IsEnabled(LogLevel.Warn.ToSerilogEventLevel()); }
        }

        public virtual void Trace(object message)
        {
            Write(LogLevel.Trace, message);
        }

        public virtual void Trace(object message, Exception exception)
        {
            if (!IsTraceEnabled)
                return;

            Write(LogLevel.Trace, exception, message);
        }

        public virtual void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteFormat(LogLevel.Trace, formatProvider, format, args);
        }

        public virtual void TraceFormat(
            IFormatProvider formatProvider,
            string format,
            Exception exception,
            params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteFormat(LogLevel.Trace, exception, formatProvider, format, args);
        }

        public virtual void TraceFormat(string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteFormat(LogLevel.Trace, format, args);
        }

        public virtual void TraceFormat(string format, Exception exception, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteFormat(LogLevel.Trace, exception, format, args);
        }

        public virtual void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsTraceEnabled)
                return;
            WriteCallback(LogLevel.Trace, formatMessageCallback);
        }

        public virtual void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsTraceEnabled)
                return;
            WriteCallback(LogLevel.Trace, formatMessageCallback, exception);
        }

        public virtual void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsTraceEnabled)
                return;
            WriteCallback(LogLevel.Trace, formatProvider, formatMessageCallback);
        }

        public virtual void Trace(
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception)
        {
            if (!IsTraceEnabled)
                return;
            WriteCallback(LogLevel.Trace, formatProvider, formatMessageCallback, exception);
        }

        public virtual void Debug(object message)
        {
            if (!IsDebugEnabled)
                return;
            Write(LogLevel.Debug, message);
        }

        public virtual void Debug(object message, Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            Write(LogLevel.Debug, exception, message);
        }

        public virtual void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteFormat(LogLevel.Debug, formatProvider, format, args);
        }

        public virtual void DebugFormat(
            IFormatProvider formatProvider,
            string format,
            Exception exception,
            params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteFormat(LogLevel.Debug, exception, formatProvider, format, args);
        }

        public virtual void DebugFormat(string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteFormat(LogLevel.Debug, format, args);
        }

        public virtual void DebugFormat(string format, Exception exception, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteFormat(LogLevel.Debug, exception, format, args);
        }

        public virtual void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsDebugEnabled)
                return;
            WriteCallback(LogLevel.Debug, formatMessageCallback);
        }

        public virtual void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            WriteCallback(LogLevel.Debug, formatMessageCallback, exception);
        }

        public virtual void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsDebugEnabled)
                return;
            WriteCallback(LogLevel.Debug, formatProvider, formatMessageCallback);
        }

        public virtual void Debug(
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            WriteCallback(LogLevel.Debug, formatProvider, formatMessageCallback, exception);
        }

        public virtual void Info(object message)
        {
            if (!IsInfoEnabled)
                return;
            Write(LogLevel.Info, message);
        }

        public virtual void Info(object message, Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            Write(LogLevel.Info, exception, message);
        }

        public virtual void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteFormat(LogLevel.Info, formatProvider, format, args);
        }

        public virtual void InfoFormat(
            IFormatProvider formatProvider,
            string format,
            Exception exception,
            params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteFormat(LogLevel.Info, exception, formatProvider, format, args);
        }

        public virtual void InfoFormat(string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteFormat(LogLevel.Info, format, args);
        }

        public virtual void InfoFormat(string format, Exception exception, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteFormat(LogLevel.Info, exception, format, args);
        }

        public virtual void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsInfoEnabled)
                return;
            WriteCallback(LogLevel.Info, formatMessageCallback);
        }

        public virtual void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            WriteCallback(LogLevel.Info, formatMessageCallback, exception);
        }

        public virtual void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsInfoEnabled)
                return;
            WriteCallback(LogLevel.Info, formatProvider, formatMessageCallback);
        }

        public virtual void Info(
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            WriteCallback(LogLevel.Info, formatProvider, formatMessageCallback, exception);
        }

        public virtual void Warn(object message)
        {
            if (!IsWarnEnabled)
                return;
            Write(LogLevel.Warn, message);
        }

        public virtual void Warn(object message, Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            Write(LogLevel.Warn, exception, message);
        }

        public virtual void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteFormat(LogLevel.Warn, formatProvider, format, args);
        }

        public virtual void WarnFormat(
            IFormatProvider formatProvider,
            string format,
            Exception exception,
            params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteFormat(LogLevel.Warn, exception, formatProvider, format, args);
        }

        public virtual void WarnFormat(string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteFormat(LogLevel.Warn, format, args);
        }

        public virtual void WarnFormat(string format, Exception exception, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteFormat(LogLevel.Warn, exception, format, args);
        }

        public virtual void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsWarnEnabled)
                return;
            WriteCallback(LogLevel.Warn, formatMessageCallback);
        }

        public virtual void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            WriteCallback(LogLevel.Warn, formatMessageCallback, exception);
        }

        public virtual void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsWarnEnabled)
                return;
            WriteCallback(LogLevel.Warn, formatProvider, formatMessageCallback);
        }

        public virtual void Warn(
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            WriteCallback(LogLevel.Warn, formatProvider, formatMessageCallback, exception);
        }

        public virtual void Error(object message)
        {
            if (!IsErrorEnabled)
                return;
            Write(LogLevel.Error, message);
        }

        public virtual void Error(object message, Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            Write(LogLevel.Error, exception, message);
        }

        public virtual void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteFormat(LogLevel.Error, formatProvider, format, args);
        }

        public virtual void ErrorFormat(
            IFormatProvider formatProvider,
            string format,
            Exception exception,
            params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteFormat(LogLevel.Error, exception, formatProvider, format, args);
        }

        public virtual void ErrorFormat(string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteFormat(LogLevel.Error, format, args);
        }

        public virtual void ErrorFormat(string format, Exception exception, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteFormat(LogLevel.Error, exception, format, args);
        }

        public virtual void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsErrorEnabled)
                return;
            WriteCallback(LogLevel.Error, formatMessageCallback);
        }

        public virtual void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            WriteCallback(LogLevel.Error, formatMessageCallback, exception);
        }

        public virtual void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsErrorEnabled)
                return;
            WriteCallback(LogLevel.Error, formatProvider, formatMessageCallback);
        }

        public virtual void Error(
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            WriteCallback(LogLevel.Error, formatProvider, formatMessageCallback, exception);
        }

        public virtual void Fatal(object message)
        {
            if (!IsFatalEnabled)
                return;
            Write(LogLevel.Fatal, message);
        }

        public virtual void Fatal(object message, Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            Write(LogLevel.Fatal, exception, message);
        }

        public virtual void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteFormat(LogLevel.Fatal, formatProvider, format, args);
        }

        public virtual void FatalFormat(
            IFormatProvider formatProvider,
            string format,
            Exception exception,
            params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteFormat(LogLevel.Fatal, exception, formatProvider, format, args);
        }

        public virtual void FatalFormat(string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteFormat(LogLevel.Fatal, format, args);
        }

        public virtual void FatalFormat(string format, Exception exception, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteFormat(LogLevel.Fatal, exception, format, args);
        }

        public virtual void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsFatalEnabled)
                return;
            WriteCallback(LogLevel.Fatal, formatMessageCallback);
        }

        public virtual void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            WriteCallback(LogLevel.Fatal, formatMessageCallback, exception);
        }

        public virtual void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsFatalEnabled)
                return;
            WriteCallback(LogLevel.Fatal, formatProvider, formatMessageCallback);
        }

        public virtual void Fatal(
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            WriteCallback(LogLevel.Fatal, formatProvider, formatMessageCallback, exception);
        }

        protected void Write(LogLevel level, object message)
        {
            Write(level, null, message);
        }

        protected void Write(LogLevel level, Exception exception, object message)
        {
            if (message is string)
                _logger.Write(level.ToSerilogEventLevel(), exception, "{Message:l}", message.ToString());
            else
                _logger.Write(level.ToSerilogEventLevel(), exception, "{@Message}", message);
        }

        protected void WriteCallback(
            LogLevel level,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception = null)
        {
            WriteCallback(level, null, formatMessageCallback, exception);
        }

        protected void WriteCallback(
            LogLevel level,
            IFormatProvider formatProvider,
            Action<FormatMessageHandler> formatMessageCallback,
            Exception exception = null)
        {
            formatMessageCallback(MakeFormatted(level, formatProvider, exception));
        }

        protected FormatMessageHandler MakeFormatted(
            LogLevel level,
            IFormatProvider formatProvider,
            Exception exception)
        {
            var messageHandler = new FormatMessageHandler(delegate(string message, object[] parameters)
            {
                string formatted = string.Format(formatProvider, message, parameters);

                if (formatProvider == null)
                    WriteFormat(level, exception, message, parameters);
                else
                    Write(level, exception, formatted);

                return formatted;
            });

            return messageHandler;
        }

        protected void WriteFormat(LogLevel level, Exception exception, string message, object[] parameters)
        {
            WriteFormat(level, exception, null, message, parameters);
        }

        protected void WriteFormat(LogLevel level, IFormatProvider formatProvider, string message, object[] parameters)
        {
            WriteFormat(level, null, formatProvider, message, parameters);
        }

        protected void WriteFormat(LogLevel level, string message, object[] parameters)
        {
            WriteFormat(level, null, null, message, parameters);
        }

        protected void WriteFormat(
            LogLevel level,
            Exception exception,
            IFormatProvider formatProvider,
            string message,
            object[] parameters)
        {
            if (formatProvider == null)
            {
                // check for non-value types and enable deconstruction on them...
                List<KeyValuePair<string, string>> replaceParameters =
                    parameters.Select((p, i) => new { Param = p, Index = i })
                        .Where(p => p != null && !p.GetType().IsValueType)
                        .Select(
                            p =>
                            new KeyValuePair<string, string>(string.Format("{{{0}}}", p.Index),
                                string.Format("{{@{0}}}", p.Index)))
                        .ToList();

                // replace format strings {0} with {@0}.
                // not very fast, but simple at least.
                message = replaceParameters.Aggregate(message,
                    (current, replace) => current.Replace(replace.Key, replace.Value));

                _logger.Write(level.ToSerilogEventLevel(), exception, message, parameters);
            }
            else
                Write(level, exception, string.Format(formatProvider, message, parameters));
        }
    }
}
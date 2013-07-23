// Copyright 2013 CaptiveAire Systems
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

    using Common.Logging.Factory;

    using global::Serilog;
    using global::Serilog.Events;

    /// <summary> Serilog common logger. </summary>
    public class SerilogCommonLogger : AbstractLogger
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Constructors and Destructors

        public SerilogCommonLogger(ILogger logger)
        {
            this._logger = logger;
        }

        #endregion

        #region Public Properties

        public override bool IsDebugEnabled
        {
            get
            {
                return this._logger.IsEnabled(this.ConvertLevel(LogLevel.Debug));
            }
        }

        public override bool IsErrorEnabled
        {
            get
            {
                return this._logger.IsEnabled(this.ConvertLevel(LogLevel.Error));
            }
        }

        public override bool IsFatalEnabled
        {
            get
            {
                return this._logger.IsEnabled(this.ConvertLevel(LogLevel.Fatal));
            }
        }

        public override bool IsInfoEnabled
        {
            get
            {
                return this._logger.IsEnabled(this.ConvertLevel(LogLevel.Info));
            }
        }

        public override bool IsTraceEnabled
        {
            get
            {
                return this._logger.IsEnabled(this.ConvertLevel(LogLevel.Trace));
            }
        }

        public override bool IsWarnEnabled
        {
            get
            {
                return this._logger.IsEnabled(this.ConvertLevel(LogLevel.Warn));
            }
        }

        #endregion

        #region Methods

        /// <summary> Actually sends the <paramref name="message" /> to the underlying log system. </summary>
        /// <param name="level"> the level of this log event. </param>
        /// <param name="message"> the message to log. </param>
        /// <param name="exception"> the exception to log (may be null) </param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            var logLevel = this.ConvertLevel(level);

            this._logger.Write(logLevel, exception, message.ToString());
        }

        /// <summary> Convert level. </summary>
        /// <param name="logLevel"> The log level. </param>
        /// <returns> The level converted. </returns>
        private LogEventLevel ConvertLevel(LogLevel logLevel)
        {
            LogEventLevel logEventLevel;
            if (!Enum.TryParse(logLevel.ToString(), true, out logEventLevel))
            {
                switch (logLevel)
                {
                    case LogLevel.All:
                        logEventLevel = LogEventLevel.Verbose;
                        break;
                    case LogLevel.Info:
                        logEventLevel = LogEventLevel.Information;
                        break;
                    case LogLevel.Warn:
                        logEventLevel = LogEventLevel.Warning;
                        break;
                }
            }

            return logEventLevel;
        }

        #endregion
    }
}
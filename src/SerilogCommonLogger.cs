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
    using System.Web.Script.Serialization;
    using System.Collections.Generic;

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

            // _logger.Write will happily log both strings and objects.
            // Objects will be logged without destroying their structure (using "structured" logging).
            // But just in case, there is a try-catch fallback to convert the message to a string
            // before trying again to log it.
            try
            {
                // If message is a string, try to deserialize if it might be a JSON object.
                // If that goes wrong, simply log it as a string.
                //
                // This is very useful, because Common.Logging implementations for other logging packages
                // often do not support logging objects - when given an object, they just log the name of the object.
                // By including this conversion, software trying to log objects via Common.Logging can
                // serialise those objects into a JSON string and log that string, knowing that if
                // Common.Logging.Serilog is used, the object will still be logged in a structured manner.

                if (message is string)
                {
                    string messageString = (string)message;

                    if (messageString.TrimStart().StartsWith("{"))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        message = js.Deserialize<Dictionary<string, Object>>(messageString);
                    }
                }

                this._logger.Write(logLevel, exception, "{message:l}", message);
            }
            catch
            {
                this._logger.Write(logLevel, exception, "{message:l}", message.ToString());
            }
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
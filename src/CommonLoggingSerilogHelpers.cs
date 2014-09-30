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

    using global::Serilog.Events;

    public static class CommonLoggingSerilogHelpers
    {
        /// <summary> Convert level. </summary>
        /// <param name="logLevel"> The log level. </param>
        /// <returns> The level converted. </returns>
        public static LogEventLevel ToSerilogEventLevel(this LogLevel logLevel)
        {
            LogEventLevel logEventLevel;
            if (Enum.TryParse(logLevel.ToString(), true, out logEventLevel))
                return logEventLevel;

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

            return logEventLevel;
        }
    }
}
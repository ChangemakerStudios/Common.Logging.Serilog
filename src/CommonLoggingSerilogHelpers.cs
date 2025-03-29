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
            if (Enum.TryParse(logLevel.ToString(), true, out LogEventLevel logEventLevel))
                return logEventLevel;

            logEventLevel = logLevel switch
            {
                LogLevel.All or LogLevel.Trace => LogEventLevel.Verbose,
                LogLevel.Info => LogEventLevel.Information,
                LogLevel.Warn => LogEventLevel.Warning,
                _ => logEventLevel
            };

            return logEventLevel;
        }
    }
}
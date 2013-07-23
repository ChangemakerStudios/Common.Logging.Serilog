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

    using global::Serilog;

    /// <summary>
    /// Serilog factory adapter.
    /// </summary>
    public class SerilogFactoryAdapter : ILoggerFactoryAdapter
    {
        /// <summary>
        /// Instance of Serilog
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger
        {
            get
            {
                return this._logger ?? Log.Logger;
            }
        }

        /// <summary>
        /// Initializes a new instance of the SerilogFactoryAdapter class.
        /// </summary>
        /// <param name="logger"> The logger.</param>
        public SerilogFactoryAdapter(ILogger logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the SerilogFactoryAdapter class.
        /// </summary>
        public SerilogFactoryAdapter()
        {
        }

        /// <summary>
        /// Get a ILog instance by type.
        /// </summary>
        /// <param name="type"> The type to use for the logger.</param>
        /// <returns>
        /// The logger.
        /// </returns>
        public ILog GetLogger(Type type)
        {
            return new SerilogCommonLogger(this.Logger.ForContext(type));
        }

        /// <summary>
        /// Get a ILog instance by name.
        /// </summary>
        /// <param name="name"> The name of the logger.</param>
        /// <returns>
        /// The logger.
        /// </returns>
        public ILog GetLogger(string name)
        {
            return new SerilogCommonLogger(this.Logger.ForContext("name", name));
        }
    }
}
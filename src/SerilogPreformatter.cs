// Copyright 2014-2017 CaptiveAire Systems
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Logging.Serilog
{
    public class SerilogPreformatter
    {
        static readonly Regex _numericFormattedRegex = new Regex(@"{(\d{1,})}", RegexOptions.Compiled);

        public bool TryPreformat(string templateString, object[] args, out string newTemplate, out object[] newArgs)
        {
            newTemplate = templateString;
            newArgs = args;

            if (string.IsNullOrEmpty(templateString))
            {
                return true;
            }

            if (args == null || !args.Any())
            {
                return true;
            }
            
            var templateBuilder = new StringBuilder(templateString);
            var filteredArgs = new List<object>(args);
            var matches = _numericFormattedRegex.Matches(templateString);

            for (var i = matches.Count - 1; i >= 0; i--)
            {
                var currentMatcher = matches[i];
                var argPosition = GetArgumentPosition(currentMatcher);
                var currentArg = args[argPosition];

                templateBuilder.Remove(currentMatcher.Index, currentMatcher.Length);
                templateBuilder.Insert(currentMatcher.Index, currentArg);

                filteredArgs.RemoveAt(argPosition);
            }

            newTemplate = templateBuilder.ToString();
            newArgs = filteredArgs.ToArray();

            return true;
        }

        static int GetArgumentPosition(Match currentMatcher)
        {
            if (currentMatcher == null)
                throw new ArgumentNullException(nameof(currentMatcher));

            return int.Parse(currentMatcher.Groups.OfType<Group>().Skip(1).First().Value);
        }
    }
}

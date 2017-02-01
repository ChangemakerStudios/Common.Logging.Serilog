using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Logging.Serilog
{
    public class SerilogPreformatter
    {
        private readonly Regex _numericFormattedRegex = new Regex(@"{(\d{1,})}", RegexOptions.Compiled);

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

        private static int GetArgumentPosition(Match currentMatcher)
        {
            var nummeric = currentMatcher.Groups
                    .OfType<Group>()
                    .Skip(1)
                    .First()
                    .Value;
            return int.Parse(nummeric);
        }
    }
}

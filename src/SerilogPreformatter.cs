using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Logging.Serilog
{
    public class SerilogPreformatter
    {
        private readonly Regex _numericFormattedRegex = new Regex(@"{(\d)}", RegexOptions.Compiled);

        public bool TryPreformat(string templateString, object[] args, out string newTemplate, out object[] newArgs)
        {
            if (string.IsNullOrEmpty(templateString))
            {
                newTemplate = templateString;
                newArgs = args;
                return true;
            }

            if (args == null || !args.Any())
            {
                newTemplate = templateString;
                newArgs = args;
                return true;
            }

            newTemplate = string.Format(templateString, args);
            newArgs = null;
            return true;
        }

        internal IEnumerable<int> GetIndecesOfNumericalFormatting(string templateString)
        {
            var matches = _numericFormattedRegex.Matches(templateString);

            for (var i = 0; i < matches.Count; i++)
            {
                var numericMatch = matches[i].Groups
                    .OfType<Group>()
                    .Skip(1)
                    .First()
                    .Value;

                yield return int.Parse(numericMatch);
            }
        }
    }
}

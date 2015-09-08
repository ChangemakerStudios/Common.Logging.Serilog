using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Logging.Serilog
{
    public class SerilogPreformatter
    {
        private readonly Regex _numericFormattedRegex = new Regex(@"{(\d{1,})}", RegexOptions.Compiled);

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

        {
                    .OfType<Group>()
                    .Skip(1)
                    .First()
                    .Value;
        }
    }
}

using System.Linq;

namespace Common.Logging.Serilog
{
    public class SerilogPreformatter
    {
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
    }
}

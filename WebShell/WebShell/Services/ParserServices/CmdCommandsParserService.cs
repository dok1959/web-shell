using System.Linq;
using System.Collections.Generic;

namespace WebShell.Services.ParserServices
{
    public class CmdCommandsParserService : ICommandsParserService
    {
        public IEnumerable<string> Parse(string input)
        {
            return input.Split('&').Where(i => !string.IsNullOrWhiteSpace(i));
        }
    }
}

using System.Collections.Generic;

namespace WebShell.Services.ParserServices
{
    public interface ICommandsParserService
    {
        IEnumerable<string> Parse(string input);
    }
}

using System.Collections.Generic;

namespace WebShell.Services.ParserServices
{
    public interface IParserService
    {
        IEnumerable<string> Parse(string input);
    }
}

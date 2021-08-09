using System.Linq;
using System.Collections.Generic;

namespace WebShell.Services.ProcessorServices
{
    public class CmdInputProcessorService : IProcessorService
    {
        public IEnumerable<string> Process(string input)
        {
            return input.Split('&').Where(i => !string.IsNullOrWhiteSpace(i));
        }
    }
}

using System.Collections.Generic;

namespace WebShell.Services.ProcessorServices
{
    public interface IProcessorService
    {
        IEnumerable<string> Process(string input);
    }
}

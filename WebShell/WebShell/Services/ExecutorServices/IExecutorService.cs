using WebShell.Models;

namespace WebShell.Services.ExecutorServices
{
    public interface IExecutorService
    {
        string Execute(string command);
    }
}

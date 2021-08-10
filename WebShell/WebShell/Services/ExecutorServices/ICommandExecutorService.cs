using WebShell.Models;

namespace WebShell.Services.ExecutorServices
{
    public interface ICommandExecutorService
    {
        string Execute(string command);
    }
}

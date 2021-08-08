using WebShell.Models;

namespace WebShell.Services
{
    public interface IExecutorService
    {
        string Execute(Command command);
    }
}

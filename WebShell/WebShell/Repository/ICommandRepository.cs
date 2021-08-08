using System.Collections.Generic;
using WebShell.Models;

namespace WebShell.Repository
{
    public interface ICommandRepository
    {
        IEnumerable<Command> GetAll();
        Command Get(long id);
        void Add(Command command);
        void Remove(Command command);
    }
}

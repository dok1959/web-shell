using System.Collections.Generic;
using WebShell.Models;

namespace WebShell.Repository
{
    public class InMemoryCommandRepository : ICommandRepository
    {
        private List<Command> _commands;

        public Command Get(long id) => _commands.Find(command => command.Id.Equals(id));
        public IEnumerable<Command> GetAll() => _commands;
        public void Add(Command command) => _commands.Add(command);
        public void Remove(Command command) => _commands.Remove(command);
    }
}

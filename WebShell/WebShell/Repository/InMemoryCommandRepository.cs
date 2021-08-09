using System.Collections.Generic;
using WebShell.Models;

namespace WebShell.Repository
{
    public class InMemoryCommandRepository : ICommandRepository
    {
        private int counterId;
        private List<Command> _commands;

        public InMemoryCommandRepository()
        {
            counterId = 0;
            _commands = new List<Command>();
        }

        public Command Get(long id) => _commands.Find(command => command.Id.Equals(id));
        public IEnumerable<Command> GetAll() => _commands;
        public void Add(Command command)
        {
            command.Id = counterId++;
            _commands.Add(command);
        }
        public void Remove(Command command) => _commands.Remove(command);
    }
}

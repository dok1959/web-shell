using System.Collections.Generic;
using WebShell.Models;

namespace WebShell.Repository
{
    public class InMemoryInstructionRepository : IRepository<Instruction>
    {
        private int idCounter;
        private List<Instruction> _instructions;

        public InMemoryInstructionRepository()
        {
            idCounter = 0;
            _instructions = new List<Instruction>();
        }

        public Instruction Get(long id) => _instructions.Find(command => command.Id.Equals(id));
        public IEnumerable<Instruction> GetAll() => _instructions;
        public void Add(Instruction item)
        {
            item.Id = idCounter++;
            _instructions.Add(item);
        }
        public void Remove(Instruction item) => _instructions.Remove(item);
    }
}

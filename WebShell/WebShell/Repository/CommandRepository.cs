using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebShell.Data;
using WebShell.Models;

namespace WebShell.Repository
{
    public class CommandRepository : ICommandRepository
    {
        private ApplicationContext _context;
        private DbSet<Command> _dbSet;

        public CommandRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<Command>();
        }

        public IEnumerable<Command> GetAll() => _dbSet.AsNoTracking();
        public Command Get(long id) => _dbSet.AsNoTracking().FirstOrDefault(command => command.Id.Equals(id));
        public void Add(Command command)
        {
            _dbSet.Add(command);
            _context.SaveChanges();
        }
        public void Remove(Command item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
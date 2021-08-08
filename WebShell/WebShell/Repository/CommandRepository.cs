using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebShell.Data;
using WebShell.Models;

namespace WebShell.Repository
{
    public class CommandRepository
    {
        private ApplicationContext _context;
        private DbSet<Command> _dbSet;

        public CommandRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<Command>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Create(Command command)
        {
            _dbSet.Add(command);
        }

        public IEnumerable<Command> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Command Get(long id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(command => command.Id.Equals(id));
        }

        public void Update(Command item)
        {
            _dbSet.Update(item);
        }

        public Command Remove(Command item)
        {
            _dbSet.Remove(item);
            return item;
        }
    }
}
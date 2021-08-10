using System.Collections.Generic;
using WebShell.Models;

namespace WebShell.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Instruction Get(long id);
        void Add(T item);
        void Remove(T item);
    }
}

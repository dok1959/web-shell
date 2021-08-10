using System.Collections.Generic;
using WebShell.Data;
using WebShell.Models;

namespace WebShell.Repository
{
    public class InstructionRepository : IRepository<Instruction>
    {
        private SqlServerContext _context;

        public InstructionRepository(SqlServerContext context)
        {
            _context = context;
        }

        public IEnumerable<Instruction> GetAll() 
        {
            ICollection<Instruction> instructions = new List<Instruction>();
            var reader = _context.ExecuteReader("SELECT * FROM Instructions;");
            while(reader.Read())
            {
                Instruction instruction = new Instruction
                {
                    Id = reader.GetInt64(0),
                    Content = reader.GetString(1)
                };
                instructions.Add(instruction);
            }
            reader.Close();
            return instructions;
        }
        public Instruction Get(long id)
        {
            var reader = _context.ExecuteReader($"SELECT * FROM Instructions WHERE Id = {id};");
            reader.Read();
            Instruction instruction = new Instruction
            {
                Id = reader.GetInt64(0),
                Content = reader.GetString(1)
            };
            reader.Close();
            return instruction;
        }
        public void Add(Instruction item)
        {
            _context.ExecuteNonQuery($"INSERT INTO Instructions (Content) VALUES ('{item.Content}');");
        }
        public void Remove(Instruction item)
        {
            _context.ExecuteNonQuery($"DELETE FROM Instructions WHERE Id = {item.Id} and Content = {item.Content};");
        }
    }
}
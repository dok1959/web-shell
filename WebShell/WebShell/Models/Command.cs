using System.ComponentModel.DataAnnotations;

namespace WebShell.Models
{
    public class Command
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebShell.Models;
using WebShell.Repository;
using WebShell.Services;

namespace WebShell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private CommandRepository _repository;
        private IExecutorService _executor;
        public CommandsController(CommandRepository repository, IExecutorService executor)
        {
            _repository = repository;
            _executor = executor;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Command> commands = _repository.GetAll();
            if (commands == null)
                return NotFound();
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(long id)
        {
            Command command = _repository.Get(id);
            if (command == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        public IActionResult Run(Command command)
        {
            string result = _executor.Execute(command);
            return Ok(result);
        }
    }
}

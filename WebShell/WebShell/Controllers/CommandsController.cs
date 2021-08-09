using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using WebShell.Models;
using WebShell.Repository;
using WebShell.Services.ExecutorServices;
using WebShell.Services.ProcessorServices;

namespace WebShell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private ICommandRepository _repository;
        private IExecutorService _executor;
        private IProcessorService _commandProcessor;
        public CommandsController(
            ICommandRepository repository, 
            IExecutorService executor, 
            IProcessorService commandProcessor)
        {
            _repository = repository;
            _executor = executor;
            _commandProcessor = commandProcessor;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Command> commands = _repository.GetAll();
            if (commands == null)
                return NotFound();
            return Ok(commands);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Command command = _repository.Get(id);
            if (command == null)
                return NotFound();
            return Ok(command);
        }

        [HttpPost]
        public IActionResult Run([FromBody] Command model)
        {
            if(model == null || string.IsNullOrEmpty(model?.Content))
            {
                return BadRequest();
            }

            _repository.Add(model);

            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<string> commands = _commandProcessor.Process(model.Content);
            foreach(var command in commands)
            {
                stringBuilder.AppendLine(_executor.Execute(command));
            }
            string result = stringBuilder.ToString().TrimEnd();

            return Ok(result);
        }
    }
}

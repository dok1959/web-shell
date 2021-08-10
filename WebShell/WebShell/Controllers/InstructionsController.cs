﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using WebShell.Models;
using WebShell.Repository;
using WebShell.Services.ExecutorServices;
using WebShell.Services.ParserServices;

namespace WebShell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructionsController : ControllerBase
    {
        private IRepository<Instruction> _repository;
        private ICommandExecutorService _commandExecutor;
        private ICommandsParserService _commandsParser;
        public InstructionsController(
            IRepository<Instruction> repository, 
            ICommandExecutorService commandExecutor, 
            ICommandsParserService commandsParser)
        {
            _repository = repository;
            _commandExecutor = commandExecutor;
            _commandsParser = commandsParser;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Instruction> instructions = _repository.GetAll();
            if (instructions == null)
                return NotFound();
            return Ok(instructions);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Instruction instruction = _repository.Get(id);
            if (instruction == null)
                return NotFound();
            return Ok(instruction);
        }

        [HttpPost]
        public IActionResult Run([FromBody] Instruction instruction)
        {
            if(instruction == null || string.IsNullOrEmpty(instruction?.Content))
            {
                return BadRequest();
            }

            _repository.Add(instruction);

            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<string> commands = _commandsParser.Parse(instruction.Content);
            foreach(var command in commands)
            {
                stringBuilder.AppendLine(_commandExecutor.Execute(command));
            }
            string result = stringBuilder.ToString().TrimEnd();

            return Ok(result);
        }
    }
}

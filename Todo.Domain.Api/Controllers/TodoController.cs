using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Application.Commands;
using Todo.Domain.Application.Handlers;
using Todo.Domain.Application.Services.Contracts;

namespace Todo.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoHandler _handler;
        private readonly ITodoService _service;

        public TodoController(TodoHandler handler, ITodoService service)
        {
            _handler = handler;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoCommand command)
        {
            var retorno = (GenericCommandResult)await _handler.Handle(command);

            if (retorno.Success)
                return Ok(retorno);

            return BadRequest(retorno);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTodoCommand command)
        {
            var retorno = (GenericCommandResult)await _handler.Handle(command);

            if (retorno.Success)
                return Ok(retorno);

            return BadRequest(retorno);
        }

        [HttpPut]
        public async Task<IActionResult> MarkAsDone([FromBody] MarkTodoAsDoneCommand command)
        {
            var retorno = (GenericCommandResult)await _handler.Handle(command);

            if (retorno.Success)
                return Ok(retorno);

            return BadRequest(retorno);
        }

        [HttpPut]
        public async Task<IActionResult> MarkAsUndone([FromBody] MarkTodoAsUndoneCommand command)
        {
            var retorno = (GenericCommandResult)await _handler.Handle(command);

            if (retorno.Success)
                return Ok(retorno);

            return BadRequest(retorno);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string user)
        {
            var todos = await _service.GetAll(user);

            if (todos.Count() > 0)
                return Ok(new GenericCommandResult(true, string.Empty, todos));

            return NotFound(new GenericCommandResult(false, string.Empty, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDone(
            [FromQuery] string user)
        {
            var todos = await _service.GetAllDone(user);

            if (todos.Count() > 0)
                return Ok(new GenericCommandResult(true, string.Empty, todos));

            return NotFound(new GenericCommandResult(false, string.Empty, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUndone(
            [FromQuery] string user)
        {
            var todos = await _service.GetAllUndone(user);

            if (todos.Count() > 0)
                return Ok(new GenericCommandResult(true, string.Empty, todos));

            return NotFound(new GenericCommandResult(false, string.Empty, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetByPeriod(
            [FromQuery] string user,
            [FromQuery] bool done,
            [FromQuery] DateTime date
            )
        {
            var todos = await _service.GetByPeriod(user, date, done);

            if (todos.Count() > 0)
                return Ok(new GenericCommandResult(true, string.Empty, todos));

            return NotFound(new GenericCommandResult(false, string.Empty, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(
            [FromQuery] Guid id,
            [FromQuery] string user)
        {
            var todo = await _service.GetById(id, user);

            if (todo is not null)
                return Ok(new GenericCommandResult(true, string.Empty, todo));

            return NotFound(new GenericCommandResult(false, string.Empty, null));
        }
    }
}

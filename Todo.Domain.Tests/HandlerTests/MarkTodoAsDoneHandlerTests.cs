using Todo.Domain.Application.Commands;
using Todo.Domain.Application.Handlers;
using Todo.Domain.Entities;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class MarkTodoAsDoneHandlerTests
    {
        private readonly CreateTodoCommand _createCommand;
        private readonly MarkTodoAsDoneCommand _invalidCommand;
        private readonly MarkTodoAsDoneCommand _validCommand;
        private readonly FakeTodoRepository _repository = new FakeTodoRepository();
        private readonly TodoHandler _todoHandler;
        TodoItem _todoItem;

        public MarkTodoAsDoneHandlerTests()
        {
            _todoHandler = new TodoHandler(_repository);
            _createCommand = new CreateTodoCommand("TESTE", "teste da silva", DateTime.Now);
            _invalidCommand = new MarkTodoAsDoneCommand();
            _validCommand = new MarkTodoAsDoneCommand();
        }

        private async Task Init()
        {
            var result = (GenericCommandResult)await _todoHandler.Handle(_createCommand);
            _todoItem = (TodoItem)result.Data;

        }

        [TestMethod]
        public async Task Given_An_Invalid_Command_Must_Interrupt_MarkAsDone_Execution()
        {
            await Init();
            var result = (GenericCommandResult)await _todoHandler.Handle(_invalidCommand);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Given_An_Valid_Command_Must_MarkAsDone_Todo()
        {
            await Init();
            _validCommand.Id = _todoItem.Id;
            _validCommand.User = _todoItem.User;
            var result = (GenericCommandResult)await _todoHandler.Handle(_validCommand);
            var todoItem = (TodoItem)result.Data;
            Assert.AreEqual(result.Success, true);
            Assert.AreEqual(todoItem.Done, true);
        }
    }
}

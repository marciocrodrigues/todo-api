using Todo.Domain.Application.Commands;
using Todo.Domain.Application.Handlers;
using Todo.Domain.Entities;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class UpdateTodoHandlerTests
    {
        private readonly CreateTodoCommand _createCommand;
        private readonly UpdateTodoCommand _invalidCommand;
        private readonly UpdateTodoCommand _validCommand;
        private readonly FakeTodoRepository _repository = new FakeTodoRepository();
        private readonly TodoHandler _todoHandler;
        TodoItem _todoItem;

        public UpdateTodoHandlerTests()
        {
            _todoHandler = new TodoHandler(_repository);
            _createCommand = new CreateTodoCommand("TESTE", "teste da silva", DateTime.Now);
            _invalidCommand = new UpdateTodoCommand(Guid.NewGuid(), "", "");
            _validCommand = new UpdateTodoCommand(Guid.NewGuid(), "TESTE", "teste da silva");
        }

        [TestMethod]
        public async Task Given_An_Invalid_Command_Must_Interrupt_Update_Execution()
        {
            await Init();
            var result = (GenericCommandResult)await _todoHandler.Handle(_invalidCommand);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Given_An_Valid_Command_Must_Update_Todo()
        {
            await Init();
            _validCommand.Id = _todoItem.Id;
            var result = (GenericCommandResult)await _todoHandler.Handle(_validCommand);
            Assert.AreEqual(result.Success, true);
        }

        private async Task Init()
        {
            var result = (GenericCommandResult)await _todoHandler.Handle(_createCommand);
            _todoItem = (TodoItem)result.Data;
        }

    }
}

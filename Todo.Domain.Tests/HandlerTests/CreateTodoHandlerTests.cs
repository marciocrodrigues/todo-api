using Todo.Domain.Application.Commands;
using Todo.Domain.Application.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("TESTE", "teste da silva", DateTime.Now);
        private readonly FakeTodoRepository _repository = new FakeTodoRepository();
        private readonly TodoHandler _todoHandler;

        public CreateTodoHandlerTests()
        {
            _todoHandler = new TodoHandler(_repository);
        }

        [TestMethod]
        public async Task Given_An_Invalid_Command_Must_Interrupt_Execution()
        {
            var result =  (GenericCommandResult)await _todoHandler.Handle(_invalidCommand);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Given_An_Valid_Command_Must_Create_Todo()
        {
            var result = (GenericCommandResult)await _todoHandler.Handle(_validCommand);
            Assert.AreEqual(result.Success, true);
        }
    }
}

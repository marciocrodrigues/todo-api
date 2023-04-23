using Todo.Domain.Application.Commands;

namespace Todo.Domain.Tests.CommandTests
{
    [TestClass]
    public class CreateTodoCommandTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("TESTE", "teste da silva", DateTime.Now);

        public CreateTodoCommandTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void Given_An_Invalid_Command()
        {
            Assert.AreEqual(_invalidCommand.Valid, false);
        }


        [TestMethod]
        public void Given_An_Valid_Command()
        {
            Assert.AreEqual(_validCommand.Valid, true);
        }
    }
}

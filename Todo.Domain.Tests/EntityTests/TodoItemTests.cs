using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntityTests
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _validTodoItem = new TodoItem("Titulo aqui", "TESTE", DateTime.Now);

        [TestMethod]
        public void Given_A_New_TodoItem_Is_Done_False()
        {

            Assert.AreEqual(_validTodoItem.Done, false);
        }
    }
}

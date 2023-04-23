using Todo.Domain.Application.Queries;
using Todo.Domain.Entities;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", "usuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 2", "usuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 3", "usuarioB", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 4", "usuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 5", "usuarioB", DateTime.Now.AddDays(1)));
        }


        [TestMethod]
        public void Given_A_Consult_With_User_Must_Returning_All_TodoItens_From_Him()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("usuarioA"));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Given_A_Consult_With_User_Must_Returning_All_TodoItens_With_Done_True()
        {
            _items[0].MarkAsDone();

            var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("usuarioA"));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Given_A_Consult_With_User_Must_Returning_All_TodoItens_With_Done_False_And_Perid_Date()
        {
            var data = DateTime.Now.AddDays(1);

            var result = _items.AsQueryable().Where(TodoQueries.GetByPeriod("usuarioB", data, false));
            Assert.AreEqual(1, result.Count());
        }
    }
}

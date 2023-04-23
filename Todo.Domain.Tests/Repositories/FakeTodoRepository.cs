using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Tests.Repositories
{
    public class FakeTodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> todos = new List<TodoItem>();

        public async Task Create(TodoItem todo)
        {
            todos.Add(todo);
            await Task.FromResult(todo);
        }

        public async Task Update(TodoItem todo)
        {
            var index = todos.FindIndex(x => x.Id == todo.Id);

            if (index > 0)
            {
                todos[index] = todo;
                await Task.FromResult(todos[index]);
            }
        }
    }
}

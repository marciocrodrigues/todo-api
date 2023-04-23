using System.Linq;
using System.Linq.Expressions;
using Todo.Domain.Application.Queries;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Tests.Repositories
{
    public class FakeTodoRepository : ITodoRepository
    {
        private List<TodoItem> todos = new List<TodoItem>();

        public async Task Create(TodoItem todo)
        {
            todos.Add(todo);
            await Task.FromResult(todo);
        }

        public async Task<TodoItem> GetById(Guid id, string user)
        {
            var todo = todos.FirstOrDefault(x => x.Id == id && x.User == user);
            return await Task.FromResult(todo);
        }

        public async Task<IEnumerable<TodoItem>> GetWithFilter(Expression<Func<TodoItem, bool>> expression)
        {
            var retorno = todos.AsQueryable().Where(expression).ToList();
            return await Task.FromResult(retorno);
        }

        public async Task<IEnumerable<TodoItem>> GetWithFilterAsNoTracking(Expression<Func<TodoItem, bool>> expression)
        {
            var retorno = todos.AsQueryable().Where(expression).ToList();
            return await Task.FromResult(retorno);
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

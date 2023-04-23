using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task Create(TodoItem todo);
        Task Update(TodoItem todo);

        Task<TodoItem> GetById(Guid id, string user);
        Task<IEnumerable<TodoItem>> GetWithFilter(Expression<Func<TodoItem, bool>> expression);
        Task<IEnumerable<TodoItem>> GetWithFilterAsNoTracking(Expression<Func<TodoItem, bool>> expression);
    }
}

using Todo.Domain.Entities;

namespace Todo.Domain.Application.Services.Contracts
{
    public interface ITodoService
    {
        Task<TodoItem> GetById(Guid id, string user);

        Task<IEnumerable<TodoItem>> GetAll(string user);
        Task<IEnumerable<TodoItem>> GetAllDone(string user);

        Task<IEnumerable<TodoItem>> GetAllUndone(string user);
        Task<IEnumerable<TodoItem>> GetByPeriod(string user, DateTime date, bool done);
    }
}

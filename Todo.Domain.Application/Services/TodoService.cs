using Todo.Domain.Application.Queries;
using Todo.Domain.Application.Services.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAll(string user)
        {
            return await _repository.GetWithFilterAsNoTracking(TodoQueries.GetAll(user));
        }

        public async Task<IEnumerable<TodoItem>> GetAllDone(string user)
        {
            return await _repository.GetWithFilterAsNoTracking(TodoQueries.GetAllDone(user));
        }

        public async Task<IEnumerable<TodoItem>> GetAllUndone(string user)
        {
            return await _repository.GetWithFilterAsNoTracking(TodoQueries.GetAllUndone(user));
        }

        public async Task<TodoItem> GetById(Guid id, string user)
        {
            return await _repository.GetById(id, user);
        }

        public async Task<IEnumerable<TodoItem>> GetByPeriod(string user, DateTime date, bool done)
        {
            return await _repository.GetWithFilterAsNoTracking(TodoQueries.GetByPeriod(user, date, done));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _context;

        public TodoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(TodoItem todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoItem todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> GetById(Guid id, string user)
        {
            return await _context.Todos.FirstOrDefaultAsync(x => x.Id == id && x.User == user);
        }

        public async Task<IEnumerable<TodoItem>> GetWithFilter(Expression<Func<TodoItem, bool>> expression)
        {
            return await _context.Todos.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetWithFilterAsNoTracking(Expression<Func<TodoItem, bool>> expression)
        {
            return await _context.Todos.AsNoTracking().Where(expression).ToListAsync();
        }
    }
}

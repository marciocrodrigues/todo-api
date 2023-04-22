using Flunt.Notifications;
using Todo.Domain.Application.Commands;
using Todo.Domain.Application.Commands.Contracts;
using Todo.Domain.Application.Handlers.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Application.Handlers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(CreateTodoCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            var todoItem = new TodoItem(command.Title, command.User, command.Date);

            await _repository.Create(todoItem);

            return new GenericCommandResult(true, "Tarefa salva", todoItem);
        }
    }
}

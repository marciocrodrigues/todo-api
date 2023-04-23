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
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
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

        public async Task<ICommandResult> Handle(UpdateTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            var todo = await _repository.GetById(command.Id, command.User);

            todo.UpdateTitle(command.Title);

            await _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public async Task<ICommandResult> Handle(MarkTodoAsDoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            var todo = await _repository.GetById(command.Id, command.User);

            todo.MarkAsDone();

            await _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public async Task<ICommandResult> Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            var todo = await _repository.GetById(command.Id, command.User);

            todo.MarkAsUndone();

            await _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}

using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Application.Commands.Contracts;

namespace Todo.Domain.Application.Commands
{
    public class MarkTodoAsDoneCommand : Notifiable, ICommand
    {
        public MarkTodoAsDoneCommand() { }
        public MarkTodoAsDoneCommand(Guid id, string user)
        {
            Id = id;
            User = user;
        }

        public Guid Id { get; set; }
        public string User { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract().Requires().HasMinLen(User, 6, "User", "Usuário inválido!"));
        }
    }
}

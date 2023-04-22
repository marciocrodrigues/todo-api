using Todo.Domain.Application.Commands.Contracts;

namespace Todo.Domain.Application.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}

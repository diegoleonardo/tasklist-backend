namespace Tasklist.Commands.Interfaces
{
    public interface IHandler<in T> where T: ICommand
    {
        CommandResult Execute(T command);
    }
}

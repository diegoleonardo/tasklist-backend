using Tasklist.Commands.Tasks;

namespace Tasklist.Commands.Interfaces
{
    public interface ITaskHandler :
        IHandler<TaskCreatorCmd>,
        IHandler<TaskUpdaterCmd>,
        IHandler<TaskUpdaterStatusCmd>,
        IHandler<TaskRemoverCmd>
    {
    }
}

using Tasklist.Domain.Entities;
using Tasklist.PersistentStorage.POCOS;

namespace Tasklist.PersistentStorage.Utils
{
    internal static class PersistenceModelToDomainModelTransformer
    {
        internal static TaskEntity Transform(Task task)
        {
            return new TaskEntity(task.Id, task.Title, task.Status, task.Description);
        }
    }
}

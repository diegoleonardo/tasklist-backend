using Tasklist.Domain.Entities;
using Tasklist.PersistentStorage.POCOS;

namespace Tasklist.PersistentStorage.Utils
{
    internal static class DomainModelToPersistenceModelTransformer
    {
        internal static Task Transform(TaskEntity taskEntity)
        {
            return new Task()
            {
                Id = taskEntity.Id,
                Title = taskEntity.Title,
                Description = taskEntity.Description,
                Status = taskEntity.Status,
                StatusDescription = taskEntity.Status.ToString()
            };
        }
    }
}

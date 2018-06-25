using Tasklist.Domain.Entities;

namespace Tasklist.Queries.Models
{
    public static class DomainTaskToDTOMapper
    {
        public static TaskDTO Transform(Task task)
        {
            return new TaskDTO()
            {
                id = task.Id,
                title = task.Title,
                description = task.Description,
                status = task.Status.ToString(),
                createdAt = task.CreatedAt,
                updatedAt = task.UpdatedAt,
                doneAt = task.DoneAt,
                inProgressAt = task.InProgressAt
            };
        }
    }
}

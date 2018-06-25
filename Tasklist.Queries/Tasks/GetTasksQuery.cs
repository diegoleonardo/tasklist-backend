using System.Collections.Generic;
using Tasklist.Domain.Entities;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;

namespace Tasklist.Queries.Tasks
{
    public class GetTasksQuery: IQuery<IEnumerable<TaskDTO>>
    {
    }
}

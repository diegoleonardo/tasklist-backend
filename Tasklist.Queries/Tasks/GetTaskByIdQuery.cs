using System;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;

namespace Tasklist.Queries.Tasks
{
    public class GetTaskByIdQuery: IQuery<TaskDTO>
    {
        public Guid Id { get; private set; }
        public GetTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

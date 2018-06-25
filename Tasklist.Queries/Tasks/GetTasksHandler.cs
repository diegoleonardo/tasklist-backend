using System.Collections.Generic;
using System.Linq;
using Tasklist.Domain.Entities;
using Tasklist.PersistentStorage.Repositories;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;

namespace Tasklist.Queries.Tasks
{
    public class GetTasksHandler : IQueryHandler<GetTasksQuery, IEnumerable<TaskDTO>>
    {
        private readonly IRepository<Task> _repository;
        public GetTasksHandler(IRepository<Task> repository)
        {
            _repository = repository;
        }
        public IEnumerable<TaskDTO> Handle(GetTasksQuery query)
        {
            var tasks = _repository.Get();
            return tasks.Select(task => DomainTaskToDTOMapper.Transform(task));
        }
    }
}

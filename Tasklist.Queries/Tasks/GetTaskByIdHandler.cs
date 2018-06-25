using Tasklist.Domain.Entities;
using Tasklist.PersistentStorage.Repositories;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;

namespace Tasklist.Queries.Tasks
{
    public class GetTaskByIdHandler : IQueryHandler<GetTaskByIdQuery, TaskDTO>
    {
        private readonly IRepository<Task> _repository;
        public GetTaskByIdHandler(IRepository<Task> repository)
        {
            _repository = repository;
        }
        public TaskDTO Handle(GetTaskByIdQuery query)
        {
            var task = _repository.GetByID(query.Id);
            return DomainTaskToDTOMapper.Transform(task);
        }
    }
}

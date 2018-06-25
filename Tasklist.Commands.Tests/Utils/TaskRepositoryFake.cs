using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tasklist.Domain.Entities;
using Tasklist.PersistentStorage.Repositories;

namespace Tasklist.Commands.Tests.Utils
{
    public class TaskRepositoryFake : IRepository<Task>
    {
        private readonly IList<Task> _storage;
        public TaskRepositoryFake()
        {
            _storage = new List<Task>();
        }
        public void Delete(object id)
        {
            var task = _storage.FirstOrDefault(x => x.Id.Equals(id));
            Delete(task);
        }

        public void Delete(Task entityToDelete)
        {
            _storage.Remove(entityToDelete);
        }

        public IEnumerable<Task> Get(Expression<Func<Task, bool>> filter = null, Func<IQueryable<Task>, IOrderedQueryable<Task>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Task> query = _storage.AsQueryable();
            return query.Where(filter).ToList();
        }

        public Task GetByID(object id)
        {
            return _storage.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Insert(Task entity)
        {
            _storage.Add(entity);
        }

        public void Update(Task entityToUpdate)
        {
            _storage.Add(entityToUpdate);
        }
    }
}

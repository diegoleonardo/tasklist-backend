using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tasklist.Domain.Entities;
using Tasklist.PersistentStorage.Contexto;
using System.Data.Entity;

namespace Tasklist.PersistentStorage.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        internal readonly TaskContext context;
        internal readonly DbSet<Task> dbSet;
        public TaskRepository()
        {
            context = new TaskContext();
            dbSet = context.Set<Task>();
        }
        public IEnumerable<Task> Get(Expression<Func<Task, bool>> filter = null, Func<IQueryable<Task>, IOrderedQueryable<Task>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Task> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public Task GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public void Insert(Task entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }
        public void Delete(object id)
        {
            var task = dbSet.Find(id);
            Delete(task);
        }
        public void Delete(Task entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }
        public void Update(Task entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}

using System;
using Tasklist.PersistentStorage.Contexto;
using Tasklist.PersistentStorage.POCOS;

namespace Tasklist.PersistentStorage.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private TaskContext _context = new TaskContext();
        private GenericRepository<Task> _taskRepository;
        public GenericRepository<Task> TaskRepository
        {
            get
            {
                if(this._taskRepository == null)
                {
                    this._taskRepository = new GenericRepository<Task>(_context);
                }
                return _taskRepository;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

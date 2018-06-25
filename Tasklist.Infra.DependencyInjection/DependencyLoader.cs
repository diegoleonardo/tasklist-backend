using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using Tasklist.Commands.Interfaces;
using Tasklist.Commands.Tasks;
using Tasklist.Domain.Entities;
using Tasklist.Infra.Logger;
using Tasklist.PersistentStorage.Repositories;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;
using Tasklist.Queries.Tasks;

namespace Tasklist.Infra.DependencyInjection
{
    public static class DependencyLoader
    {
        public static Container LoadDependency()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<ILog, ExceptionLogger>(Lifestyle.Scoped);
            container.Register<IRepository<Task>, TaskRepository>(Lifestyle.Scoped); 
            container.Register<ITaskHandler, TaskHandler>(Lifestyle.Scoped);
            container.Register<IQueryHandler<GetTasksQuery, IEnumerable<TaskDTO>>, GetTasksHandler>(Lifestyle.Scoped);

            return container;
        }
    }
}

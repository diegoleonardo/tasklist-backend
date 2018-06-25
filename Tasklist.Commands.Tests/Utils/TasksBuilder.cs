using System;
using Tasklist.Commands.Enums;
using Tasklist.Commands.Tasks;
using Tasklist.Domain.Entities;
using Tasklist.Infra.Logger;
using Tasklist.PersistentStorage.Repositories;

namespace Tasklist.Commands.Tests.Utils
{
    public static class TasksBuilder
    {
        public static TaskHandler CreateTaskHandler(IRepository<Task> tasksList, ILog logger)
        {
            return new TaskHandler(tasksList, logger);
        }
        public static TaskCreatorCmd CreateTaskCreatorCmd(string title, string description)
        {
            return new TaskCreatorCmd(title, description);
        }
        public static TaskUpdaterCmd CreateTaskUpdaterCmd(Guid id, string title, string description)
        {
            return new TaskUpdaterCmd(id, title, description);
        }
        public static TaskUpdaterStatusCmd CreateTaskUpdaterStatusCmd(Guid Id, TaskStatusCmd status)
        {
            return new TaskUpdaterStatusCmd(Id, status);
        }
        public static TaskRemoverCmd CreateTaskRemoverCmd(Guid id)
        {
            return new TaskRemoverCmd(id);
        }
    }
}

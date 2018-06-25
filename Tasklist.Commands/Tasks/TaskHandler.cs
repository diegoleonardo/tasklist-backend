using System;
using Tasklist.Commands.Interfaces;
using Tasklist.Commands.Messages;
using Tasklist.Domain.Entities;
using Tasklist.Domain.Enums;
using Tasklist.Infra.Logger;
using Tasklist.PersistentStorage.Repositories;

namespace Tasklist.Commands.Tasks
{
    /// <summary>
    /// Class that handles events in Tasks
    /// </summary>
    public class TaskHandler : ITaskHandler
    {
        private readonly IRepository<Task> _repository;
        private readonly ILog _logger;
        public TaskHandler(IRepository<Task> repository, ILog logger)
        {
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// Function that creates a new Task
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public CommandResult Execute(TaskCreatorCmd newTask)
        {
            try
            {
                if (!newTask.IsCommandValid())
                {
                    var message = newTask.ReturnInvalidNotifications();
                    return new CommandResult(false, message);
                }
                var task = new Task(Guid.NewGuid(), newTask.Title, TaskStatus.CREATED, newTask.Description);
                _repository.Insert(task);
                return new CommandResult(true, SuccessMessages.OPERATION_SUCCESS);
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw new Exception(FailMessages.EXCEPTION_FAIL_MESSAGE);
            }
        }
        /// <summary>
        /// Function that updates values of task's Title and description
        /// </summary>
        /// <param name="taskToUpdate"></param>
        /// <returns>Result of Operation</returns>
        public CommandResult Execute(TaskUpdaterCmd taskToUpdate)
        {
            try
            {
                if (!taskToUpdate.IsCommandValid())
                {
                    var message = taskToUpdate.ReturnInvalidNotifications();
                    return new CommandResult(false, message);
                }
                var task = _repository.GetByID(taskToUpdate.Id);
                if (task == null)
                {
                    return new CommandResult(false, ValidationMessages.TASK_NOT_FOUND_TO_PROCESS);
                }
                task.ChangeTitle(taskToUpdate.Title);
                task.ChangeDescription(taskToUpdate.Description);
                _repository.Update(task);
                return new CommandResult(true, SuccessMessages.OPERATION_SUCCESS);
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw new Exception(FailMessages.EXCEPTION_FAIL_MESSAGE);
            }
        }
        /// <summary>
        /// Function that changes status of task to IN_PROGRESS or DONE
        /// </summary>
        /// <param name="taskToUpdateStatus"></param>
        /// <returns></returns>
        public CommandResult Execute(TaskUpdaterStatusCmd taskToUpdateStatus)
        {
            try
            {
                if (!taskToUpdateStatus.IsCommandValid())
                {
                    var message = taskToUpdateStatus.ReturnInvalidNotifications();
                    return new CommandResult(false, message);
                }
                var task = _repository.GetByID(taskToUpdateStatus.Id);
                if (task == null)
                {
                    return new CommandResult(false, ValidationMessages.TASK_NOT_FOUND_TO_PROCESS);
                }
                if (taskToUpdateStatus.ShouldStatusBeChangedToDone())
                {
                    task.SetToDone();
                }
                else
                {
                    task.SetToInProgress();
                }
                _repository.Update(task);
                return new CommandResult(true, SuccessMessages.OPERATION_SUCCESS);
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw new Exception(FailMessages.EXCEPTION_FAIL_MESSAGE);
            }
        }
        /// <summary>
        /// Function that removes a task
        /// </summary>
        /// <param name="taskToDelete"></param>
        /// <returns></returns>
        public CommandResult Execute(TaskRemoverCmd taskToDelete)
        {
            try
            {
                if (!taskToDelete.IsCommandValid())
                {
                    var message = taskToDelete.ReturnInvalidNotifications();
                    return new CommandResult(false, message);
                }
                var task = _repository.GetByID(taskToDelete.Id);
                if (task == null)
                {
                    return new CommandResult(false, ValidationMessages.TASK_NOT_FOUND_TO_PROCESS);
                }
                _repository.Delete(task.Id);
                return new CommandResult(true, SuccessMessages.OPERATION_SUCCESS);
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                throw new Exception(FailMessages.EXCEPTION_FAIL_MESSAGE);
            }
        }
    }
}

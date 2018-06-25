using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Tasklist.Commands.Messages;
using Tasklist.Commands.Tasks;
using Tasklist.Commands.Tests.Utils;
using Tasklist.Domain.Entities;
using Tasklist.Domain.Enums;
using Tasklist.Infra.Logger;
using Tasklist.PersistentStorage.Repositories;

namespace Tasklist.Commands.Tests.TaskHandles
{
    [TestClass]
    public class TaskRemoverHandlerTests
    {
        private Task _defaulTask;
        private IRepository<Task> _tasksList;
        private Mock<ILog> _mockLogger = new Mock<ILog>();
        private TaskHandler _taskHandler;
        [TestInitialize]
        public void Init()
        {
            _tasksList = new TaskRepositoryFake();
            _defaulTask = new Task(Guid.NewGuid(), "Task Example One", TaskStatus.CREATED);
            var taskTwo = new Task(Guid.NewGuid(), "Task Example Two", TaskStatus.CREATED);
            var taskThree = new Task(Guid.NewGuid(), "Task Example Three", TaskStatus.IN_PROGRESS);
            var taskFour = new Task(Guid.NewGuid(), "Task Example Four", TaskStatus.DONE);
            _tasksList.Insert(_defaulTask);
            _tasksList.Insert(taskTwo);
            _tasksList.Insert(taskThree);
            _tasksList.Insert(taskFour);
            _taskHandler = TasksBuilder.CreateTaskHandler(_tasksList, _mockLogger.Object);
        }
        [TestMethod]
        public void ShouldBeDeletedATask()
        {
            // Arrange
            var taskId = _defaulTask.Id;
            var taskRemoverCmd = TasksBuilder.CreateTaskRemoverCmd(taskId);

            // Act
            var result = _taskHandler.Execute(taskRemoverCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            var task = _tasksList.GetByID(taskId);
            Assert.IsNull(task);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultFalseWhenTryToRemoveTaskNotFound()
        {
            // Arrange
            var expectedMessage = ValidationMessages.TASK_NOT_FOUND_TO_PROCESS;
            var taskUpdateCmd = TasksBuilder.CreateTaskRemoverCmd(Guid.NewGuid());

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}

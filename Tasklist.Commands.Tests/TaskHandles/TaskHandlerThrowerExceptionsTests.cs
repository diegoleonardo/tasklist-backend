using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Tasklist.Commands.Messages;
using Tasklist.Commands.Tasks;
using Tasklist.Commands.Tests.Utils;
using Tasklist.Domain.Entities;
using Tasklist.Infra.Logger;
using Tasklist.PersistentStorage.Repositories;

namespace Tasklist.Commands.Tests.TaskHandles
{
    [TestClass]
    public class TaskHandlerThrowerExceptionsTests
    {
        private Mock<IRepository<Task>> _repositoryTask;
        private Mock<ILog> _mockLogger = new Mock<ILog>();
        private Exception _exception = new Exception("There was an error in the infrastructure");
        private TaskHandler _taskHandler;
        [TestInitialize]
        public void Init()
        {
            _repositoryTask = new Mock<IRepository<Task>>();
            _taskHandler = TasksBuilder.CreateTaskHandler(_repositoryTask.Object, _mockLogger.Object);
        }
        [TestMethod]
        public void ShouldBeThrownAExceptionWhenCreateATask()
        {
            // Arrange
            _repositoryTask.Setup(x => x.Insert(It.IsAny<Task>())).Throws(_exception);
            var taskCreatorCmd = TasksBuilder.CreateTaskCreatorCmd("Task One", "Task description");

            // Assert
            var exceptionThrowed = Assert.ThrowsException<Exception>(() => _taskHandler.Execute(taskCreatorCmd));
            Assert.AreEqual(FailMessages.EXCEPTION_FAIL_MESSAGE, exceptionThrowed.Message);
        }
        [TestMethod]
        public void ShouldBeThrownAExceptionWhenUpdateTask()
        {
            // Arrange
            _repositoryTask.Setup(x => x.GetByID(It.IsAny<object>())).Throws(_exception);
            var taskUpdaterCmd = TasksBuilder.CreateTaskUpdaterCmd(Guid.NewGuid(), "Task One", "Task one description");

            // Assert
            var exceptionThrowed = Assert.ThrowsException<Exception>(() => _taskHandler.Execute(taskUpdaterCmd));
            Assert.AreEqual(FailMessages.EXCEPTION_FAIL_MESSAGE, exceptionThrowed.Message);
        }
        [TestMethod]
        public void ShouldBeThrownAExceptionWhenUpdateTaskStatus()
        {
            // Arrange
            var taskStatusUpdaterCmd = TasksBuilder.CreateTaskUpdaterStatusCmd(Guid.NewGuid(), 
                Enums.TaskStatusCmd.IN_PROGRESS);
            _repositoryTask.Setup(x => x.Update(It.IsAny<Task>())).Throws(_exception);
            _repositoryTask.Setup(x => x.GetByID(It.IsAny<object>()))
                .Returns(new Task(taskStatusUpdaterCmd.Id, "Task to update status", Domain.Enums.TaskStatus.CREATED));

            // Assert
            var exceptionThrowed = Assert.ThrowsException<Exception>(() => _taskHandler.Execute(taskStatusUpdaterCmd));
            Assert.AreEqual(FailMessages.EXCEPTION_FAIL_MESSAGE, exceptionThrowed.Message);
        }
    }
}

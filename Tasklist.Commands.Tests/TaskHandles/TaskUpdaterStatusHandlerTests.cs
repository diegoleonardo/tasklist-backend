using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Tasklist.Commands.Enums;
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
    public class TaskUpdaterStatusHandlerTests
    {
        private Task _defaulTask;
        private IRepository<Task> _tasksList;
        private Mock<ILog> _mockLogger = new Mock<ILog>();
        private TaskStatus _initialStatus = TaskStatus.CREATED;
        private TaskHandler _taskHandler;
        [TestInitialize]
        public void Init()
        {
            _tasksList = new TaskRepositoryFake();
            _defaulTask = new Task(Guid.NewGuid(), "Task Example One", _initialStatus);
            _tasksList.Insert(_defaulTask);
            _taskHandler = TasksBuilder.CreateTaskHandler(_tasksList, _mockLogger.Object);
        }
        [TestMethod]
        public void ShouldBeChangedTaskStatusToInProgress()
        {
            // Arrange
            var taskInProgressCmd = TasksBuilder.CreateTaskUpdaterStatusCmd(_defaulTask.Id, TaskStatusCmd.IN_PROGRESS);
            var expectedStatus = TaskStatus.IN_PROGRESS;

            // Act
            var result = _taskHandler.Execute(taskInProgressCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreNotEqual(_initialStatus, _defaulTask.Status);
            Assert.AreEqual(expectedStatus, _defaulTask.Status);
        }
        [TestMethod]
        public void ShouldBeChangedTaskStatusToDone()
        {
            // Arrange
            var taskDoneCmd = TasksBuilder.CreateTaskUpdaterStatusCmd(_defaulTask.Id, TaskStatusCmd.DONE);
            var expectedStatus = TaskStatus.DONE;

            // Act
            var result = _taskHandler.Execute(taskDoneCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreNotEqual(_initialStatus, _defaulTask.Status);
            Assert.AreEqual(expectedStatus, _defaulTask.Status);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultFalseWhenTryToUpdateStatusTaskNotFound()
        {
            // Arrange
            var expectedMessage = ValidationMessages.TASK_NOT_FOUND_TO_PROCESS;
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterStatusCmd(Guid.NewGuid(), TaskStatusCmd.DONE);

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}

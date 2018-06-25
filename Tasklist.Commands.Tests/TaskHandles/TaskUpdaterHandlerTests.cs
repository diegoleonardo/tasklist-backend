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
    public class TaskUpdaterHandlerTests
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
            _tasksList.Insert(_defaulTask);
            _taskHandler = TasksBuilder.CreateTaskHandler(_tasksList, _mockLogger.Object);
        }
        [TestMethod]
        public void ShouldBeChangedTaskTitle()
        {
            // Arrange
            var newTitle = "New task Title Changed";
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterCmd(_defaulTask.Id, newTitle, "Task one description");

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(newTitle, _defaulTask.Title);
        }
        [TestMethod]
        public void ShouldBeChangedTaskDescription()
        {
            // Arrange
            var newDescription = "New description of task";
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterCmd(_defaulTask.Id, "Task One", newDescription);

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(newDescription, _defaulTask.Description);
        }
        [TestMethod]
        public void ShouldBeChangedTaskTitleAndDescription()
        {
            // Arrange
            var newTitle = "New task title";
            var newDescription = "New description of task";
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterCmd(_defaulTask.Id, newTitle, newDescription);

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(newTitle, _defaulTask.Title);
            Assert.AreEqual(newDescription, _defaulTask.Description);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultFalseWhenTryUpdateTaskWithIdEmpty()
        {
            // Arrange
            var expectedMessage = $"{ValidationMessages.ID_NULL}\r\n";
            var title = "New task title";
            var description = "New description of task";
            Guid id = Guid.Empty;
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterCmd(id, title, description);

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultFalseWhenTryUpdateTaskWithTitleNull()
        {
            // Arrange
            var expectedMessage = $"{ValidationMessages.EMPTY_TITLE}\r\n";
            var description = "New description of task";
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterCmd(Guid.NewGuid(), "", description);

            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultFalseWhenTryToUpdateTaskNotFound()
        {
            // Arrange
            var expectedMessage = ValidationMessages.TASK_NOT_FOUND_TO_PROCESS;
            var taskUpdateCmd = TasksBuilder.CreateTaskUpdaterCmd(Guid.NewGuid(), "Task to update", "");
 
            // Act
            var result = _taskHandler.Execute(taskUpdateCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}

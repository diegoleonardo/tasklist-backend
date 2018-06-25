using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tasklist.Commands.Messages;
using Tasklist.Commands.Tasks;
using Tasklist.Commands.Tests.Utils;
using Tasklist.Domain.Entities;
using Tasklist.Infra.Logger;
using Tasklist.PersistentStorage.Repositories;

namespace Tasklist.Commands.Tests.TaskHandles
{
    [TestClass]
    public class TaskCreatorHandlerTests
    {
        private IRepository<Task> _taskList;
        private Mock<ILog> _mockLogger = new Mock<ILog>();
        private TaskHandler _taskHandler;
        [TestInitialize]
        public void Init()
        {
            _taskList = new TaskRepositoryFake();
            _taskHandler = TasksBuilder.CreateTaskHandler(_taskList, _mockLogger.Object);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultSuccessWhenExecuteCommandWithValidData()
        {
            // Arrange
            var taskCreatorCmd = TasksBuilder.CreateTaskCreatorCmd("Task One", "Task description");
            var expectedMessage = SuccessMessages.OPERATION_SUCCESS;

            // Act
            var result = _taskHandler.Execute(taskCreatorCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
        [TestMethod]
        public void ShouldBeReturnedOperationResultFalseWhenExecuteCommandWithInvalidData()
        {
            // Arrange
            var taskCreatorCmd = TasksBuilder.CreateTaskCreatorCmd("", "");
            var expectedMessage = $"{ValidationMessages.EMPTY_TITLE}\r\n";

            // Act
            var result = _taskHandler.Execute(taskCreatorCmd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}

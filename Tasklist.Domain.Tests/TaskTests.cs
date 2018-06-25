using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tasklist.Domain.Entities;
using Tasklist.Domain.Enums;

namespace Tasklist.Domain.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void ShouldBeCreatedATaskObjectWithStatusCreated()
        {
            // Arrange
            var title = "Task One";

            // Act
            Task task = CreateTask(Guid.NewGuid(), title);
            
            // Assert
            Assert.IsNotNull(task);
            Assert.IsFalse(string.IsNullOrWhiteSpace(task.Title));
            Assert.AreEqual(TaskStatus.CREATED, task.Status);
            Assert.IsTrue(string.IsNullOrWhiteSpace(task.Description));
            Assert.IsTrue(task.CreatedAt != null);
            Assert.IsFalse(task.UpdatedAt != null);
            Assert.IsFalse(task.DoneAt != null);
        }
        [TestMethod]
        public void ShouldBeChangedATaskToInProgress()
        {
            // Arrange
            Task task = CreateTask(Guid.NewGuid(), "Task One");
            var initialStatusTask = task.Status;

            // Act
            task.SetToInProgress();

            // Assert
            Assert.AreEqual(TaskStatus.IN_PROGRESS, task.Status);
            Assert.AreNotEqual(initialStatusTask, task.Status);
            Assert.IsTrue(task.InProgressAt != null);
        }
        [TestMethod]
        public void ShouldBeChangedATaskTitle()
        {
            // Arrange
            Task task = CreateTask(Guid.NewGuid(), "Task One");
            var newTitle = "Task Two";

            // Act
            task.ChangeTitle(newTitle);

            // Assert
            Assert.AreEqual(newTitle, task.Title);
            Assert.IsTrue(task.UpdatedAt != null);
        }
        [TestMethod]
        public void ShouldBeCreatedATaskWithDescription()
        {
            // Arrange
            var description = "Initial task to work.";

            // Act
            Task task = CreateTask(Guid.NewGuid(), "Task One", TaskStatus.CREATED, description);

            // Arrange
            Assert.IsFalse(string.IsNullOrWhiteSpace(task.Description));
            Assert.AreEqual(description, task.Description);
            Assert.IsTrue(task.CreatedAt != null);
        }
        [TestMethod]
        public void ShouldBeChangedATaskDescription()
        {
            // Arrange
            Task task = CreateTask(Guid.NewGuid(), "Task One");
            var newDescription = "Some new description";

            // Act
            task.ChangeDescription(newDescription);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(task.Description));
            Assert.AreEqual(newDescription, task.Description);
            Assert.IsTrue(task.UpdatedAt != null);
        }
        [TestMethod]
        public void ShouldBeDeletedATask()
        {
            // Arrange
            Task task = CreateTask(Guid.NewGuid(), "Task One");

            // Act
            task.DeleteTask();

            // Assert
            Assert.AreEqual(TaskStatus.DELETED, task.Status);
        }
        [TestMethod]
        public void ShouldBeChangedATaskToDone()
        {
            // Arrange
            Task task = CreateTask(Guid.NewGuid(), "Task One");

            // Act
            task.SetToDone();

            // Assert
            Assert.AreEqual(TaskStatus.DONE, task.Status);
            Assert.IsTrue(task.DoneAt != null);
        }
        [TestMethod]
        public void ShouldBeAutoGeneratedIdWhenTryToPassGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            var task = CreateTask(id, "Task One");

            // Assert
            Assert.AreNotEqual(id, task.Id);
        }
        [TestMethod]
        public void ShouldBeThrowArgumentExceptionWhenCreateATaskTitleWithNullValue()
        {
            // Arrange
            string title = null;

            // Assert
            Assert.ThrowsException<ArgumentException>(() => CreateTask(Guid.NewGuid(), title));
        }
        [TestMethod]
        public void ShouldBeThrowArgumentExceptionWhenCreateATaskTitleWithWhitespace()
        {
            // Arrange
            var title = string.Empty;

            // Assert
            Assert.ThrowsException<ArgumentException>(() => CreateTask(Guid.NewGuid(), title));
        }
        [TestMethod]
        public void ShouldNotBeChangedTitleWhenTryToPassANullValue()
        {
            // Arrange
            string title = null;
            var task = CreateTask(Guid.NewGuid(), "Task one");
            var updatedAt = task.UpdatedAt;
            var titleOnCreation = task.Title;

            // Act
            task.ChangeTitle(title);

            // Assert
            Assert.AreEqual(titleOnCreation, task.Title);
            Assert.AreEqual(updatedAt, task.UpdatedAt);
        }
        [TestMethod]
        public void ShouldNotBeChangedTitleWhenTryToPassAEmptyValue()
        {
            // Arrange
            string title = string.Empty;
            var task = CreateTask(Guid.NewGuid(), "Task one");
            var updatedAt = task.UpdatedAt;
            var titleOnCreation = task.Title;

            // Act
            task.ChangeTitle(title);

            // Assert
            Assert.AreEqual(titleOnCreation, task.Title);
            Assert.AreEqual(updatedAt, task.UpdatedAt);
        }
        [TestMethod]
        public void ShouldNotBeChangedDescriptionWhenTryToPassRepeatedValue()
        {
            // Arrange
            var description = "Description to test";
            var task = CreateTask(Guid.NewGuid(), "Task one", TaskStatus.CREATED, "Description to test");
            var updatedAt = task.UpdatedAt;

            // Act
            task.ChangeDescription(description);

            // Assert
            Assert.AreEqual(updatedAt, task.UpdatedAt);
        }
        [TestMethod]
        public void ShouldBeChangedDescriptionWhenTryToPassADifferentValueOfNullValueExistent()
        {
            // Arrange
            string descriptionNull = null;
            var description = "Description to test";
            var task = CreateTask(Guid.NewGuid(), "Task one", TaskStatus.CREATED, descriptionNull);
            var updatedAt = task.UpdatedAt;

            // Act
            task.ChangeDescription(description);

            // Assert
            Assert.AreEqual(description, task.Description);
            Assert.AreNotEqual(updatedAt, task.UpdatedAt);
        }
        private Task CreateTask(Guid id, string title, TaskStatus status = TaskStatus.CREATED, string description = "")
        {
            return new Task(id, title, status, description);
        }
    }
}

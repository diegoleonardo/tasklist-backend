using System;
using Tasklist.Domain.Enums;

namespace Tasklist.Domain.Entities
{
    /// <summary>
    /// Domain Class that represents a Task
    /// </summary>
    public class Task
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public TaskStatus Status { get; private set; }
        public string Description { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? InProgressAt { get; private set; }
        public DateTime? DoneAt { get; private set; }
        private Task() { }
        public Task(Guid id, string title, TaskStatus status, string description = "")
        {
            SetId(id);
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("title");
            }
            Title = title;
            Description = description;
            SetStatus(status);
        }
        public void ChangeTitle(string newTitle)
        {
            if (IsTitleValidToUpdate(newTitle))
            {
                Title = newTitle;
                UpdatedAt = DateTime.UtcNow;
            }
        }
        public void ChangeDescription(string newDescription)
        {
            if (IsDescriptionValidToUpdate(newDescription))
            {
                Description = newDescription;
                UpdatedAt = DateTime.UtcNow;
            }
        }
        public void SetToInProgress()
        {
            SetStatus(TaskStatus.IN_PROGRESS);
            InProgressAt = DateTime.UtcNow;
        }
        public void SetToDone()
        {
            SetStatus(TaskStatus.DONE);
            DoneAt = DateTime.UtcNow;
        }
        public void DeleteTask()
        {
            SetStatus(TaskStatus.DELETED);
        }
        private void SetId(Guid id)
        {
            if(id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = id;
            }
        }
        private void SetStatus(TaskStatus status)
        {
            if (status == TaskStatus.CREATED)
            {
                CreatedAt = DateTime.UtcNow;
            }
            Status = status;
        }
        private bool IsTitleValidToUpdate(string newTitle)
        {
            return !string.IsNullOrWhiteSpace(newTitle) && !Title.Equals(newTitle);
        }
        private bool IsDescriptionValidToUpdate(string newDescription)
        {
            return newDescription != Description;
        }
    }
}

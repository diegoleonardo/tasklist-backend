using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text;
using Tasklist.Commands.Interfaces;
using Tasklist.Commands.Messages;

namespace Tasklist.Commands.Tasks
{
    public class TaskUpdaterCmd : Notifiable, IValidatable, ICommand
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskUpdaterCmd(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNull(Id, "Id", ValidationMessages.ID_NULL)
                .IsFalse(Id == Guid.Empty, "Id", ValidationMessages.ID_NULL)
                .IsNotNullOrEmpty(Title, "Title", ValidationMessages.EMPTY_TITLE));
        }
        public virtual string ReturnInvalidNotifications()
        {
            var notificationMessage = new StringBuilder();
            foreach (var notification in Notifications)
            {
                notificationMessage.AppendLine(notification.Message);
            }

            return notificationMessage.ToString();
        }
        public virtual bool IsCommandValid()
        {
            Validate();
            return Valid;
        }
    }
}

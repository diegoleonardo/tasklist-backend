using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text;
using Tasklist.Commands.Interfaces;
using Tasklist.Commands.Messages;

namespace Tasklist.Commands.Tasks
{
    public class TaskRemoverCmd : Notifiable, IValidatable, ICommand
    {
        public Guid Id { get; private set; }
        public TaskRemoverCmd(Guid id)
        {
            Id = id;
        }
        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNull(Id, "Id", ValidationMessages.ID_NULL));
        }
        public string ReturnInvalidNotifications()
        {
            var notificationMessage = new StringBuilder();
            foreach (var notification in Notifications)
            {
                notificationMessage.AppendLine(notification.Message);
            }

            return notificationMessage.ToString();
        }
        public bool IsCommandValid()
        {
            Validate();
            return Valid;
        }
    }
}

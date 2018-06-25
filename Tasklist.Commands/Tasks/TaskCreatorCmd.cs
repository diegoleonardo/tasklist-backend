using Flunt.Notifications;
using Flunt.Validations;
using System.Text;
using Tasklist.Commands.Interfaces;
using Tasklist.Commands.Messages;

namespace Tasklist.Commands.Tasks
{
    public class TaskCreatorCmd: Notifiable, IValidatable, ICommand
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskCreatorCmd(string title, string description)
        {
            Title = title;
            Description = description;
        }
        public void Validate()
        {
            AddNotifications(new Contract()
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

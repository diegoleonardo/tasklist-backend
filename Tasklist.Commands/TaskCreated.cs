using System;

namespace Tasklist.Commands
{
    public class TaskCreated
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? InProgressAt { get; set; }
        public DateTime? DoneAt { get; set; }
        public TaskCreated()
        {
            status = "CREATED";
        }
    }
}

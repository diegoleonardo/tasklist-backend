using System;

namespace Tasklist.Queries.Models
{
    public class TaskDTO
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? inProgressAt { get; set; }
        public DateTime? doneAt { get; set; }
    }
}

using System;
using Tasklist.Commands.Enums;

namespace Tasklist.RESTfulApi.Models
{
    public class StateUpdaterViewModel
    {
        public Guid Id { get; set; }
        public TaskStatusCmd Status { get; set; }
    }
}
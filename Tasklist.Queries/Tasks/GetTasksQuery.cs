﻿using System.Collections.Generic;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;

namespace Tasklist.Queries.Tasks
{
    public class GetTasksQuery: IQuery<IEnumerable<TaskDTO>>
    {
    }
}

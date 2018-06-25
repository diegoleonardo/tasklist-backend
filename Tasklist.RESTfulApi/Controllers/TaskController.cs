using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Tasklist.Commands.Interfaces;
using Tasklist.Commands.Tasks;
using Tasklist.Queries.Interfaces;
using Tasklist.Queries.Models;
using Tasklist.Queries.Tasks;
using Tasklist.RESTfulApi.Models;

namespace Tasklist.RESTfulApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        private readonly ITaskHandler _taskHandler;
        private readonly IQueryHandler<GetTasksQuery, IEnumerable<TaskDTO>> _getTasksQueryHandler;
        private readonly IQueryHandler<GetTaskByIdQuery, TaskDTO> _getTaskByIdQueryHandler;
        public TaskController(ITaskHandler taskHandler, IQueryHandler<GetTasksQuery, 
            IEnumerable<TaskDTO>> getTasksQueryHandler,
            IQueryHandler<GetTaskByIdQuery, TaskDTO> getTaskByIdQueryHandler)
        {
            _taskHandler = taskHandler;
            _getTasksQueryHandler = getTasksQueryHandler;
            _getTaskByIdQueryHandler = getTaskByIdQueryHandler;
        }
        public IHttpActionResult Get()
        {
            try
            {
                var query = new GetTasksQuery();
                var tasks = _getTasksQueryHandler.Handle(query);

                return Json<object>(tasks);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var query = new GetTaskByIdQuery(id);
                var tasks = _getTaskByIdQueryHandler.Handle(query);

                return Json<object>(tasks);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
        public IHttpActionResult Post([FromBody]TaskViewModel taskCreator)
        {
            try
            {
                var taskCmd = new TaskCreatorCmd(taskCreator.Title, taskCreator.Description);
                var result = _taskHandler.Execute(taskCmd);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                return Created("Task", result.Data);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
        public IHttpActionResult Put(Guid id, [FromBody]TaskViewModel taskViewModel)
        {
            try
            {
                var taskUpdaterCmd = new TaskUpdaterCmd(id, taskViewModel.Title, taskViewModel.Description);
                var result = _taskHandler.Execute(taskUpdaterCmd);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }

            return Ok();
        }
        public IHttpActionResult PATCH([FromBody] StateUpdaterViewModel stateUpdaterViewModel)
        {
            try
            {
                var taskUpdaterStatusCmd = new TaskUpdaterStatusCmd(stateUpdaterViewModel.Id, stateUpdaterViewModel.Status);
                var result = _taskHandler.Execute(taskUpdaterStatusCmd);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }

            return Ok();
        }
        public IHttpActionResult Delete([FromUri]Guid id)
        {
            try
            {
                var taskRemoverCmd = new TaskRemoverCmd(id);
                var result = _taskHandler.Execute(taskRemoverCmd);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }

            return Ok();
        }
    }
}

using Gevlee.Swallow.Api.Contract.Tasks;
using Gevlee.Swallow.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Api.Extensions.Mappers
{
    public static class TaskMappersExtensions
    {
        public static Task ToEntity(this TaskModel taskModel)
        {
            return new Task
            {
                Id = taskModel.Id,
                Name = taskModel.Name,
                Date = taskModel.Date
            };
        }

        public static TaskModel ToModel(this Task entity)
        {
            return new TaskModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date
            };
        }

        public static TaskModel ToModel(this Task entity, IEnumerable<TaskActivity> activities)
        {
            var taskModel = entity.ToModel();
            var notEndedActivity = activities.OrderByDescending(x => x.StartTime).FirstOrDefault(x => x.EndTime == null);
            taskModel.IsActive = notEndedActivity != null;
            taskModel.ActiveSince = notEndedActivity?.StartTime;
            taskModel.ElapsedSeconds = activities.Where(x => x.EndTime.HasValue).Select(x => x.EndTime.Value - x.StartTime).Sum(x => x.TotalSeconds);
            return taskModel;
        }
    }
}

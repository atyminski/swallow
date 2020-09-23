using Gevlee.Swallow.Api.Contract.Tasks;
using Gevlee.Swallow.Core.Entities;

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
    }
}

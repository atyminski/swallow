using Gevlee.Swallow.Api.Contract.Tags;
using Gevlee.Swallow.Core.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Gevlee.Swallow.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagRespository tagRespository;

        public TagsController(ITagRespository tagRespository)
        {
            this.tagRespository = tagRespository;
        }

        [HttpGet("{id}")]
        public ActionResult<TagModel> GetById(int id)
        {
            var task = tagRespository.Get(id);
            if (task != null)
            {
                return task.ToModel(taskActivityRepository.FindByTaskId(id));
            }
            else
            {
                return NotFound();
            }
        }
    }
}

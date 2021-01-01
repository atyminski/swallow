using Gevlee.Swallow.Api.Contract.Tags;
using Gevlee.Swallow.Core.Persistence.Repository;
using Gevlee.Swallow.Server.Extensions.Mappers;
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
            var tag = tagRespository.Get(id);
            if (tag != null)
            {
                return tagRespository.Get(id).ToModel();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

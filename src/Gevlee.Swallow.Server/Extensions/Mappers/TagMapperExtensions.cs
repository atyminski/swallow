using Gevlee.Swallow.Api.Contract.Tags;
using Gevlee.Swallow.Core.Entities;

namespace Gevlee.Swallow.Server.Extensions.Mappers
{
    public static class TagMapperExtensions
    {
        public static Tag ToEntity(this TagModel model)
        {
            return new Tag
            {
                Id = model.Id,
                Value = model.Value
            };
        }

        public static TagModel ToModel(this Tag entity)
        {
            return new TagModel
            {
                Id = entity.Id,
                Value = entity.Value
            };
        }
    }
}

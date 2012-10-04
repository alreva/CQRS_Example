using System.Collections.Generic;
using CQRS;

namespace ReadModel
{
    public class CategoryDto : Dto
    {
        public CategoryDto(
            string id,
            string title,
            params CategoryDto[] subCategories)
        {
            SubCategories = subCategories;
            Title = title;
            Id = id;
        }

        public string Id { get; private set; }

        public string Title { get; private set; }

        public IEnumerable<CategoryDto> SubCategories { get; private set; }
    }
}
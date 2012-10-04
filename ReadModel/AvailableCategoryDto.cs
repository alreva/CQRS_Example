using CQRS;

namespace ReadModel
{
    public class AvailableCategoryDto : Dto
    {
        public AvailableCategoryDto(
            string id,
            string title)
        {
            Title = title;
            Id = id;
        }

        public string Id { get; private set; }

        public string Title { get; private set; }
    }
}


namespace Client
{
    public class UpdateProductCategory
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string? Description { get; set; }
    }
}



namespace TestApiServer.Persistence.Dto.ProductCategory.Commands
{
    public class CreateProductCategory
    {
        public required string Name {  get; set; }
        public required string? Description { get; set; }
    }
}

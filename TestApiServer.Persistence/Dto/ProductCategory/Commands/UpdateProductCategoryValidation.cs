using FluentValidation;


namespace TestApiServer.Persistence.Dto.ProductCategory.Commands
{
   
    public class ApdateProductCategoryValidation : AbstractValidator<UpdateProductCategory>
    {
        private const int MaxLengthName = 100;
        private const int MaxLengthDescription = 400;

        public ApdateProductCategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxLengthName);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(MaxLengthDescription);
        }
    }
}

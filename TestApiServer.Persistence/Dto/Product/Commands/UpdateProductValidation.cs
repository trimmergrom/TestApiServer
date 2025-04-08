using FluentValidation;


namespace TestApiServer.Persistence.Dto.Product.Commands
{

    public class UpdateProductValidation : AbstractValidator<UpdateProduct>
    {
        private const int MaxLengthName = 100;
        private const int MaxLengthDescription = 400;

        public UpdateProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxLengthName);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(MaxLengthDescription);
            RuleFor(x => x.Price).GreaterThan(0).LessThan(1_000_000);
            RuleFor(x => x.IdProductCategory).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

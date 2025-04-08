using FluentValidation;


namespace TestApiServer.Persistence.Dto.Product.Commands
{

    public class CreateProductValidation : AbstractValidator<CreateProduct>
    {
        private const int MaxLengthName = 100;
        private const int MaxLengthDescription = 400;      

        public CreateProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxLengthName);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(MaxLengthDescription);
            RuleFor(x => x.Price).GreaterThan(0).LessThan(1_000_000);
        }
    }
}

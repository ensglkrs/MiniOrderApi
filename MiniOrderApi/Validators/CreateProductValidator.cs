using FluentValidation;
using MiniOrderApi.DTOs.Product;

namespace MiniOrderApi.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product names cannot be empty!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("The product price must be greater than 0.");
        }
    }
}
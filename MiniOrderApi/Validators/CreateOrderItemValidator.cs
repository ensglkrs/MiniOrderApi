using FluentValidation;
using MiniOrderApi.DTOs.Order;

namespace MiniOrderApi.Validators
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemRequest>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be valid.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be at least 1.");
        }
    }
}
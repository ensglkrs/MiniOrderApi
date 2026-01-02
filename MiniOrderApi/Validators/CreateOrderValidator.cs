using FluentValidation;
using MiniOrderApi.DTOs.Order;

namespace MiniOrderApi.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Items).NotEmpty().WithMessage("Order must contain at least one item.");
            RuleForEach(x => x.Items).SetValidator(new CreateOrderItemValidator());
        }
    }
}
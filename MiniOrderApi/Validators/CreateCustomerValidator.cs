using FluentValidation;
using MiniOrderApi.DTOs.Customer;

namespace MiniOrderApi.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email address.");
        }
    }
}
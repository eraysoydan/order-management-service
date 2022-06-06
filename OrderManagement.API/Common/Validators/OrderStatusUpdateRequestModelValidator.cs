using FluentValidation;
using OrderManagement.API.Models;

namespace OrderManagement.API.Common.Validators
{
    public class OrderStatusUpdateRequestModelValidator : AbstractValidator<OrderStatusUpdateRequestModel>
    {
        public OrderStatusUpdateRequestModelValidator()
        {
            RuleFor(x => x.CustomerOrderNo)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.StatusId)
                .NotNull()
                .NotEmpty();
        }
    }
}

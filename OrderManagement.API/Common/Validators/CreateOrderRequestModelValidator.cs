using FluentValidation;
using OrderManagement.API.Models;

namespace OrderManagement.API.Common.Validators
{
    public class CreateOrderRequestModelValidator : AbstractValidator<CreateOrderRequestModel>
    {
        public CreateOrderRequestModelValidator()
        {
            var quantityUnits = new string[] { "Adet", "Koli", "Paket", "Palet" };
            var weightUnits = new string[] { "Kg", "Ton" };

            RuleForEach(x => x.CreateOrderRequestItems).ChildRules(items => {

                items.RuleFor(x => x.CustomerOrderNo)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(50);

                items.RuleFor(x => x.OutputAddress)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(250);

                items.RuleFor(x => x.DestinationAddress)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(250);

                items.RuleFor(x => x.Quantity)
                    .NotNull()
                    .NotEmpty();

                items.RuleFor(x => x.QuantityUnit)
                    .NotNull()
                    .NotEmpty()
                    .Must(x => quantityUnits.Contains(x))
                    .WithMessage("'{PropertyName}' is not supported. Please only use these values:" + $"[{ string.Join(", ", quantityUnits)}]")
                    .MaximumLength(25);

                items.RuleFor(x => x.Weight)
                    .NotNull()
                    .NotEmpty();

                items.RuleFor(x => x.WeightUnit)
                    .NotNull()
                    .NotEmpty()
                    .Must(x => weightUnits.Contains(x))
                    .WithMessage("'{PropertyName}' is not supported. Please only use these values:" + $"[{ string.Join(", ", weightUnits)}]")
                    .MaximumLength(25);

                items.RuleFor(x => x.OrderItem.Code)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(25);

                items.RuleFor(x => x.OrderItem.Name)
                    .NotNull()
                    .NotEmpty()
                    .MaximumLength(50);

                items.RuleFor(x => x.Note)
                    .MaximumLength(250);
            });
        }
    }
}

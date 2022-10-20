using E_CommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace E_CommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please don't put empty product name section")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Please put product name between 5-150 charachters");

            RuleFor(p => p.Stock)
               .NotEmpty()
               .NotNull()
                .WithMessage("Product stock cannot be empty")
                .Must(s => s >= 0)
                .WithMessage("Please enter valid stock");
            RuleFor(p=>p.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product stock cannot be empty")
                .Must(s => s >= 0)
                .WithMessage("Please enter valid price");

        }
    }
}

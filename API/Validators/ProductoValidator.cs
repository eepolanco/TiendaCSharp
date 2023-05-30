using API.DTO;
using FluentValidation;

namespace API.Validators
{
    public class ProductoValidator : AbstractValidator<ProductoAddUpdateDto>
    {
        public ProductoValidator()
        {
            RuleFor(producto => producto.Nombre)
                .NotNull().WithMessage("Nombre is required.")
                .MinimumLength(10).WithMessage("Nombre must be at least 10 characters long.");

            RuleFor(producto => producto.FechaCreacion)
                .NotNull().NotEmpty().WithMessage("FechaCreacion is required.");
        }
    }
}

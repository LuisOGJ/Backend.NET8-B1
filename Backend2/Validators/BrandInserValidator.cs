using Backend2.DTOs;
using Backend2.DTOs.BrandDTOs;
using FluentValidation;

namespace Backend2.Validators
{
    public class BrandInserValidator : AbstractValidator<BrandInsertDTOs>
    {
        public BrandInserValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir entre 2 y 20 caracteres");
        }

    }
}

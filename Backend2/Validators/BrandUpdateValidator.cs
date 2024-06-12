using Backend2.DTOs.BrandDTOs;
using FluentValidation;

namespace Backend2.Validators
{
    public class BrandUpdateValidator : AbstractValidator<BrandUpdateDto>
    {

        public BrandUpdateValidator() { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nobmre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir entre 2 a 20 caracteres");
        }

    }
}

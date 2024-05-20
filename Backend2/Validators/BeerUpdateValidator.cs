using Backend2.DTOs;
using FluentValidation;

namespace Backend2.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {

        public BeerUpdateValidator() {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio | Error personalizado en BeerInsertValidator");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir entre 2 o 20 caracteres");
            RuleFor(x => x.BrandID).NotNull().WithMessage(x => "La marca es obligatoria");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "Error con valor enviado de marca");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "El {PropertyName} debe ser mayor a 0");
        }

    }
}

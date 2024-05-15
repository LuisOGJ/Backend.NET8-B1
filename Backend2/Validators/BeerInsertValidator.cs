using Backend2.DTOs;
using FluentValidation;

namespace Backend2.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {

        public BeerInsertValidator() {
            RuleFor(x => x.Name).NotEmpty();
        }

    }
}

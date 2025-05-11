using FluentValidation;
using Museum.Application.SQRS.Souvenirs.Commands.CreateSouvenir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.CQRS.Souvenirs.Commands.CreateSouvenir
{
    public class CreateSouvenirCommandValidator : AbstractValidator<CreateSouvenirCommand>
    {
        public CreateSouvenirCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Description).NotEmpty().MaximumLength(1000);
            RuleFor(c => c.Price).NotEmpty().InclusiveBetween(1, 1000000);
            RuleFor(c => c.Count).NotEmpty().InclusiveBetween(1, 100000);
        }
    }
}

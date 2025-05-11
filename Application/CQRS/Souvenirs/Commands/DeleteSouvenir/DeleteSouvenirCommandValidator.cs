using FluentValidation;
using Museum.Application.SQRS.Souvenirs.Commands.DeleteSouvenir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.CQRS.Souvenirs.Commands.DeleteSouvenir
{
    public class DeleteSouvenirCommandValidator : AbstractValidator<DeleteSouvenirCommand>
    {
        public DeleteSouvenirCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}

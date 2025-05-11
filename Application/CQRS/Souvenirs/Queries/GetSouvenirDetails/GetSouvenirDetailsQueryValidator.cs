using FluentValidation;
using Museum.Application.SQRS.Souvenirs.Queries.GetSouvenirDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.CQRS.Souvenirs.Queries.GetSouvenirDetails
{
    public class GetSouvenirDetailsQueryValidator : AbstractValidator<GetSouvenirDetailsQuery>
    {
        public GetSouvenirDetailsQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}

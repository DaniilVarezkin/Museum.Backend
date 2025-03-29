using FluentValidation;
using Museum.Application.MuseumServices.Queries.GetMuseumServiceDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Events.Queries.GetEventDetails
{
    class GetEventDetailsQueryValidator : AbstractValidator<GetEventDetailsQuery>
    {
        public GetEventDetailsQueryValidator() 
        {
            RuleFor(eventDetailsQuery => eventDetailsQuery.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

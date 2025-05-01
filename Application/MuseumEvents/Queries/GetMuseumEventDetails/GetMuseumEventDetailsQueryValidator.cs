using FluentValidation;

namespace Museum.Application.MuseumEvents.Queries.GetMuseumEventDetails
{
    public class GetMuseumEventDetailsQueryValidator : AbstractValidator<GetMuseumEventDetailsQuery>
    {
        public GetMuseumEventDetailsQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}

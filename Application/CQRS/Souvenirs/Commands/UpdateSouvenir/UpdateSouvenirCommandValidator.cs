using FluentValidation;
using Museum.Application.SQRS.Souvenirs.Commands.UpdateSouvenir;


namespace Museum.Application.CQRS.Souvenirs.Commands.UpdateSouvenir
{
    public class UpdateSouvenirCommandValidator : AbstractValidator<UpdateSouvenirCommand>
    {
        public UpdateSouvenirCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Description).NotEmpty().MaximumLength(1000);
            RuleFor(c => c.Price).NotEmpty().InclusiveBetween(1, 1000000);
            RuleFor(c => c.Count).NotEmpty().InclusiveBetween(1, 100000);
        }
    }
}

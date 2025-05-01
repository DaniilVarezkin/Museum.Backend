using FluentValidation;
using Museum.Domain;
using Museum.Application.MuseumEvents.Common;

namespace Museum.Application.MuseumEvents.Commands.CreateMuseumEvent
{
    public class CreateMuseumEventCommandValidator : AbstractValidator<CreateMuseumEventCommand>
    {
        public CreateMuseumEventCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(250);
            RuleFor(command => command.Description).NotEmpty();
            RuleFor(command => command.Annotation).NotEmpty();

            RuleFor(command => command.AudienceType)
                .IsInEnum()
                .WithMessage("Invalid AudienceType value");

            RuleFor(command => command.EventType)
                .IsInEnum()
                .WithMessage("Invalid MuseumEventType value");

            RuleFor(command => command.StartDate).NotEmpty();

            RuleFor(command => command.EndDate)
                .Must((command, endDate) => command.StartDate <= command.EndDate)
                .When(command => command.EndDate.HasValue)
                .WithMessage("EndDate must be greater than or equal to StartDate");

            RuleForEach(command => command.Photos).SetValidator(new EventPhotoUploadDtoValidator());
        }
    }
}

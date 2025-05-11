using FluentValidation;
using Museum.Application.SQRS.MuseumEvents.Common;


namespace Museum.Application.SQRS.MuseumEvents.Commands.UpdateMuseumEvent
{
    public class UpdateMuseumEventCommandValidatior : AbstractValidator<UpdateMuseumEventCommand>
    {
        public UpdateMuseumEventCommandValidatior()
        {
            RuleFor(command => command.Id).NotEmpty();

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

            RuleForEach(command => command.AddedPhotos).SetValidator(new PhotoUploadDtoValidator());
            RuleForEach(command => command.DeletedPhotosIds).NotEmpty();
        }
    }
}

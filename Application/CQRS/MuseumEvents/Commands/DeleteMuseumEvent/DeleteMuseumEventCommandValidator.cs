using FluentValidation;

namespace Museum.Application.SQRS.MuseumEvents.Commands.DeleteMuseumEvent
{
    public class DeleteMuseumEventCommandValidator : AbstractValidator<DeleteMuseumEventCommand>
    {
        public DeleteMuseumEventCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}

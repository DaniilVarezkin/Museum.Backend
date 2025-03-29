using FluentValidation;
using Museum.Application.MuseumServices.Commands.UpdateMuseumService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator() 
        {
            RuleFor(updateEventCommand =>
                updateEventCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(updateEventCommand =>
                updateEventCommand.Id).NotEqual(Guid.Empty);
        }
    }
}

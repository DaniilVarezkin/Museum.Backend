using FluentValidation;
using Museum.Application.MuseumServices.Commands.DeleteMuseumService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Events.Commands.DeleteEvent
{
    class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidator() 
        {
            RuleFor(deleteEventCommand => deleteEventCommand.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

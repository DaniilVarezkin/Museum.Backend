using FluentValidation;
using Museum.Application.MuseumServices.Commands.CreateMuseumService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator() 
        { 
            RuleFor(createEventCommand =>
                createEventCommand.Title).NotEmpty().MaximumLength(250);
        }
    }
}

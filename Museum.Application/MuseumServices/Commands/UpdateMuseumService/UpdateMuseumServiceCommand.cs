﻿using MediatR;


namespace Museum.Application.MuseumServices.Commands.UpdateMuseumService
{
    public class UpdateMuseumServiceCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

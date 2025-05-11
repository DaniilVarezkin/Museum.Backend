using MediatR;

namespace Museum.Application.SQRS.Souvenirs.Commands.DeleteSouvenir
{
    public class DeleteSouvenirCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

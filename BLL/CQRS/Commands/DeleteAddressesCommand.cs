using BLL.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteAddressesCommand : IRequest<bool>
    {
        public int idAddress;
    }

    public class DeleteAddressesCommandHandler : IRequestHandler<DeleteAddressesCommand, bool>
    {
        private IAddressesService _addressesService;
        public DeleteAddressesCommandHandler(IAddressesService addressesService)
        {
            _addressesService = addressesService;
        }

        public async Task<bool> Handle(DeleteAddressesCommand request, CancellationToken cancellationToken)
        {
            return await _addressesService.DeleteAddressesAsync(request.idAddress, cancellationToken);
        }
    }
}

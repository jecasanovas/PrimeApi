using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateAddressesCommand : IRequest<int>
    {
        public AddressesDto addressDto;
    }

    public class UpdateAddressesCommandHandler : IRequestHandler<UpdateAddressesCommand, int>
    {
        private IAddressesService _addressesService;
        private IMapper _mapper;
        public UpdateAddressesCommandHandler(IAddressesService addressesService, IMapper mapper)
        {
            _addressesService = addressesService;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateAddressesCommand request, CancellationToken cancellationToken)
        {
            return await _addressesService.UpdateAddressesAsync(_mapper.Map<Adresses>(request.addressDto), cancellationToken);
        }
    }
}

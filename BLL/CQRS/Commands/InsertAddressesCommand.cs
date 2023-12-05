using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertAddressesCommand : IRequest<int>
    {
        public AddressesDto AddressDto;
    }

    public class InsertAddressesCommandHandler : IRequestHandler<InsertAddressesCommand, int>
    {
        private IAddressesService _addressesService;
        private IMapper _mapper;
        public InsertAddressesCommandHandler(IAddressesService addressesService, IMapper mapper)
        {
            _addressesService = addressesService;
            _mapper = mapper;
        }

        public async Task<int> Handle(InsertAddressesCommand request, CancellationToken cancellationToken)
        {
            return await _addressesService.InsertAddressesAsync(_mapper.Map<Adresses>(request.AddressDto), cancellationToken);
        }
    }
}

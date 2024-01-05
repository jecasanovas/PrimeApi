using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetAddressesQuery : IRequest<DataResults<AddressesDto>>
    {
        public SearchParamsAddresses searchParams;
    }

    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, DataResults<AddressesDto>>
    {
        private IAddressesService _addressesService;
        private IMapper _mapper;
        public GetAddressesQueryHandler(IAddressesService addressesService, IMapper mapper)
        {
            _addressesService = addressesService;
            _mapper = mapper;
        }

        public async Task<DataResults<AddressesDto>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _addressesService.GetAddressessAsync(request.searchParams, cancellationToken);

            var nrows = await _addressesService.GetTotalRowsAsync(request.searchParams, cancellationToken);

            return new DataResults<AddressesDto>()
            {
                Dto = _mapper.Map<IEnumerable<AddressesDto>>(courses),
                Results = nrows
            };
        }
    }
}

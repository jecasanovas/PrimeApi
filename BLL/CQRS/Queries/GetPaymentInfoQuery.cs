using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetPaymentInfoQuery : IRequest<DataResults<PaymentInfoDto>>
    {
        public SearchParamsPaymentInfo searchParams;
    }

    public class GetPaymentInfoQueryHandler : IRequestHandler<GetPaymentInfoQuery, DataResults<PaymentInfoDto>>
    {
        private IPaymentInfoService _paymentInfoService;
        private IMapper _mapper;
        public GetPaymentInfoQueryHandler(IPaymentInfoService paymentInfoService, IMapper mapper)
        {
            _paymentInfoService = paymentInfoService;
            _mapper = mapper;
        }

        public async Task<DataResults<PaymentInfoDto>> Handle(GetPaymentInfoQuery request, CancellationToken cancellationToken)
        {
            var paymentResult = await _paymentInfoService.GetPaymentAsync(request.searchParams, cancellationToken);
            //Count results without pagination active, for paging info
            var nrows = await _paymentInfoService.GetTotalRowsAsync(request.searchParams, cancellationToken);

            return new DataResults<PaymentInfoDto>()
            {
                Dto = _mapper.Map<IEnumerable<PaymentInfoDto>>(paymentResult),
                Results = nrows
            };

        }
    }

}
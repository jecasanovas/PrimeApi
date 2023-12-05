using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using BLL.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertPaymentInfoCommand : IRequest<int>
    {
        public PaymentInfoDto PaymentInfo;
    }

    public class InsertPaymentInfoCommandHandler : IRequestHandler<InsertPaymentInfoCommand, int>
    {
        public readonly IPaymentInfoService _paymentInfoService;
        public readonly IMapper _mapper;
        public InsertPaymentInfoCommandHandler(IPaymentInfoService paymentInfoService, IMapper mapper)
        {
            _paymentInfoService = paymentInfoService;
            _mapper = mapper;
        }
        public async Task<int> Handle(InsertPaymentInfoCommand request, CancellationToken cancellationToken)
        {
            return await _paymentInfoService.InsertPaymentAsync(_mapper.Map<PaymentInfo>(request.PaymentInfo), cancellationToken);
        }
    }
}

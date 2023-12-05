using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdatePaymentInfoCommand : IRequest<int>
    {
        public PaymentInfoDto PaymentInfo;
    }

    public class UpdatePaymentInfoCommandHandler : IRequestHandler<UpdatePaymentInfoCommand, int>
    {
        public readonly IPaymentInfoService _paymentInfoService;
        public readonly IMapper _mapper;
        public UpdatePaymentInfoCommandHandler(IPaymentInfoService paymentInfoService, IMapper mapper)
        {
            _paymentInfoService = paymentInfoService;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdatePaymentInfoCommand request, CancellationToken cancellationToken)
        {
            return await _paymentInfoService.UpdatePaymentAsync(_mapper.Map<PaymentInfo>(request.PaymentInfo), cancellationToken);
        }
    }
}

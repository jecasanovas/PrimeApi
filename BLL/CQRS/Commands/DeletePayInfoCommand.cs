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
    public record DeletePaymentInfoCommand : IRequest<bool>
    {
        public int idPaymentInfo;
    }

    public class DeletePaymentInfoCommandHandler : IRequestHandler<DeletePaymentInfoCommand, bool>
    {
        public readonly IPaymentInfoService _paymentInfoService;
        public readonly IMapper _mapper;
        public DeletePaymentInfoCommandHandler(IPaymentInfoService paymentInfoService, IMapper mapper)
        {
            _paymentInfoService = paymentInfoService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeletePaymentInfoCommand request, CancellationToken cancellationToken)
        {
            return await _paymentInfoService.DeletePaymentAsync(request.idPaymentInfo, cancellationToken);
        }
    }
}

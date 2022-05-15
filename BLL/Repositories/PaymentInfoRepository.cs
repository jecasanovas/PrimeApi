using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Services;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class PaymentInfoRepository : IPaymentInfoRepository
    {
        private readonly IPaymentInfo _paymenService;
        private readonly IMapper _mapper;


        public PaymentInfoRepository(IPaymentInfo paymentService, IMapper mapper)
        {
            _paymenService = paymentService;
            _mapper = mapper;
        }

        public async Task<int> DeletePayments(int id)
        {
            return await _paymenService.DeletePayments(id);
        }

        public async Task<IEnumerable<PaymentInfo>> GetPayments(SearchParamsPaymentInfo searchParams)
        {
           return await _paymenService.GetPayments(searchParams);
        }

        public async Task<int> GetTotalRowsAsysnc(SearchParamsPaymentInfo searchParams)
        {
            return await _paymenService.GetTotalRowsAsysnc(searchParams);
        }

        public async Task<PaymentInfo> InsertPayments(PaymentInfoDto paymentInfo)
        {
           return  await _paymenService.InsertPayments(_mapper.Map<PaymentInfo>(paymentInfo));
        }

        public async Task<PaymentInfo> UpdatePayments(PaymentInfoDto paymentInfo)
        {
            return await _paymenService.UpdatePayments(_mapper.Map<PaymentInfo>(paymentInfo));
        }
    }
}

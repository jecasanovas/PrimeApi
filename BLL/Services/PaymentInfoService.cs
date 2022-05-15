using AutoMapper;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PaymentInfoService : IPaymentInfo
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<PaymentInfo> _paymentInfo;

        public PaymentInfoService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<PaymentInfo> paymentInfo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentInfo = paymentInfo;
        }
           
        public async Task<int> DeletePayments(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var addrese = await _paymentInfo.GetByIdAsync(id);

            _unitOfWork.Repository<PaymentInfo>().Delete(addrese);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return 1;
        }

        public async Task<IEnumerable<PaymentInfo>> GetPayments(SearchParamsPaymentInfo SearchParams)
        {
            return await _paymentInfo.ListAsync(new PaymentInfoParam(SearchParams));
        }

        public async Task<int> GetTotalRowsAsysnc(SearchParamsPaymentInfo SearchParams)
        { 
            return await _paymentInfo.CountAsync(new PaymentInfoParam(SearchParams, true));
        }

        public async Task<PaymentInfo> InsertPayments(PaymentInfo paymentInfo)
        {
            return await UpdatePayments(paymentInfo);
        }

        public async Task<PaymentInfo> UpdatePayments(PaymentInfo paymentInfo)
        {
            await _unitOfWork.BeginTransactionAsync();

            if (paymentInfo.Id > 0)
                _unitOfWork.Repository<PaymentInfo>().Update(paymentInfo);
            else
                _unitOfWork.Repository<PaymentInfo>().Add(paymentInfo);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return paymentInfo;
        }
    }
}




using AutoMapper;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Parameters;
using BLL.SearchParams;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PaymentInfoService : IPaymentInfoService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PaymentInfo> _paymentInfo;

        public PaymentInfoService(IUnitOfWork unitOfWork, IGenericRepository<PaymentInfo> paymentInfo)
        {
            _unitOfWork = unitOfWork;
            _paymentInfo = paymentInfo;
        }

        public async Task<bool> DeletePaymentAsync(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var payment = await _paymentInfo.GetByIdAsync(id);
            _unitOfWork.Repository<PaymentInfo>().Delete(payment);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<PaymentInfo>> GetPaymentAsync(SearchParamsPaymentInfo SearchParams, CancellationToken cancellationToken)
        {
            var criteria = new PaymentInfoParam(SearchParams);
            return await _paymentInfo.ListAsync(criteria, cancellationToken);
        }

        public async Task<int> GetTotalRowsAsync(SearchParamsPaymentInfo searchParams, CancellationToken cancellationToken)
        {
            var criteria = new PaymentInfoParam(searchParams, true);
            return await _paymentInfo.CountAsync(criteria, cancellationToken);
        }

        public async Task<int> InsertPaymentAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken)
        {
            return await UpdatePaymentAsync(paymentInfo, cancellationToken);
        }

        public async Task<int> UpdatePaymentAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (paymentInfo.Id > 0)
                _unitOfWork.Repository<PaymentInfo>().Update(paymentInfo);
            else
                _unitOfWork.Repository<PaymentInfo>().Add(paymentInfo);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return paymentInfo.Id;
        }
    }
}




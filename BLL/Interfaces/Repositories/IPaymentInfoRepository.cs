using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Repositories
{
    public interface IPaymentInfoRepository
    {

            Task<PaymentInfo> InsertPayments(PaymentInfoDto paymentInfo);
            Task<PaymentInfo> UpdatePayments(PaymentInfoDto paymentInfo);
            Task<int> GetTotalRowsAsysnc(SearchParamsPaymentInfo searchParams);

            Task<int> DeletePayments(int id);
            Task<IEnumerable<PaymentInfo>> GetPayments(SearchParamsPaymentInfo searchParams);
        }
    }




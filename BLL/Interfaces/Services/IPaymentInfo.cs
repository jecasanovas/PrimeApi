using BLL.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IPaymentInfo
    {
        Task<PaymentInfo> InsertPayments(PaymentInfo paymentInfo);
        Task<PaymentInfo> UpdatePayments(PaymentInfo paymentInfo);
        Task<int> GetTotalRowsAsysnc(SearchParamsPaymentInfo searchParams);

        Task<int> DeletePayments(int id);
        Task<IEnumerable<PaymentInfo>> GetPayments(SearchParamsPaymentInfo searchParams);
    }
}

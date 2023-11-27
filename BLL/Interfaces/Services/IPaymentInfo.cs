using BLL.SearchParams;
using Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IPaymentInfo
    {
        Task<PaymentInfo> InsertPaymentAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken);
        Task<PaymentInfo> UpdatePaymentAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsync(SearchParamsPaymentInfo searchParams, CancellationToken cancellationToken);
        Task<int> DeletePaymentAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<PaymentInfo>> GetPaymentAsync(SearchParamsPaymentInfo searchParams, CancellationToken cancellationToken);
    }
}

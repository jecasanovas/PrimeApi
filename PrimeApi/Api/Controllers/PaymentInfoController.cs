using API.Helpers;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.SearchParams;
using Core.Entities;
using Courses.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimeApi.Api.Controllers
{
    [Authorize]
    public class PaymentInfoController : BaseApiController
    {

        private readonly IPaymentInfo _paymentInfoService;
        public readonly IMapper _mapper;

        public PaymentInfoController(IMapper mapper, IPaymentInfo paymentInfoService)
        {
            _paymentInfoService = paymentInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<PaymentInfoDto>>> GetAddresses([FromQuery] SearchParamsPaymentInfo searchParameters, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _paymentInfoService.GetPaymentAsync(searchParameters, cancellationToken);
                IEnumerable<PaymentInfoDto> data = _mapper.Map<IEnumerable<PaymentInfoDto>>(result);
                var rows = await _paymentInfoService.GetTotalRowsAsync(searchParameters, cancellationToken);

                return new Pagination<BLL.Dtos.PaymentInfoDto>(searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<BLL.Dtos.PaymentInfoDto>)data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdatePaymentInfo([FromBody] PaymentInfoDto paymentInfo, CancellationToken cancellationToken)
        {

            try
            {
                var payment = await _paymentInfoService.UpdatePaymentAsync(_mapper.Map<PaymentInfo>(paymentInfo), cancellationToken);
                return Ok(_mapper.Map<PaymentInfoDto>(payment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


        [HttpPost]
        public async Task<ActionResult> InsertPaymentInfo([FromBody] PaymentInfoDto paymentInfo)
        {

            try
            {
                var payment = await _paymentInfoService.InsertPaymentAsync(_mapper.Map<PaymentInfo>(paymentInfo), CancellationToken.None);
                return Ok(_mapper.Map<PaymentInfoDto>(payment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


        [HttpDelete]
        public async Task<ActionResult<int>> DeletePaymentInfo([FromQuery] int id)
        {
            try
            {


                return Ok(await _paymentInfoService.DeletePaymentAsync(id, CancellationToken.None));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}

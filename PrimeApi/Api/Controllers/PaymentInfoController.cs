using API.Helpers;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.Models;
using Core.Entities;
using Courses.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimeApi.Api.Controllers
{
    [Authorize]
    public class PaymentInfoController : BaseApiController
    {

        private readonly IPaymentInfo _paymentInfo;
        public readonly IMapper _mapper;

        public PaymentInfoController(IMapper mapper, IPaymentInfo paymentInfo)
        {
            _paymentInfo = paymentInfo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<PaymentInfoDto>>> GetAddresses([FromQuery] SearchParamsPaymentInfo searchParameters)
        {
            try
            {
                var result = await _paymentInfo.GetPayments(searchParameters);
                IEnumerable<PaymentInfoDto> data = _mapper.Map<IEnumerable<PaymentInfoDto>>(result);
                var rows = await _paymentInfo.GetTotalRowsAsysnc(searchParameters);

                return new Pagination<BLL.Dtos.PaymentInfoDto>(searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<BLL.Dtos.PaymentInfoDto>)data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdatePaymentInfo([FromBody] PaymentInfoDto paymentInfo)
        {

            try
            {
                var payment = await _paymentInfo.UpdatePayments(_mapper.Map<PaymentInfo>(paymentInfo));
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
                var payment = await _paymentInfo.InsertPayments(_mapper.Map<PaymentInfo>(paymentInfo));
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

                return Ok(await _paymentInfo.DeletePayments(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
    }
}

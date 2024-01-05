using API.Helpers;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.SearchParams;
using Courses.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrimeApi.Api.Controllers
{
    [Authorize]
    public class PaymentInfoController : BaseApiController
    {
        public readonly IMediator _mediator;
        public PaymentInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Paginator<PaymentInfoDto>>> GetPaymentInfo([FromQuery] SearchParamsPaymentInfo searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetPaymentInfoQuery()
                {
                    searchParams = searchParameters
                });
                return new Paginator<PaymentInfoDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<PaymentInfoDto>)result.Dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdatePaymentInfo([FromBody] PaymentInfoDto paymentInfo)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdatePaymentInfoCommand()
                {
                    PaymentInfo = paymentInfo
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertPaymentInfo([FromBody] PaymentInfoDto paymentInfo)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertPaymentInfoCommand()
                {
                    PaymentInfo = paymentInfo
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePaymentInfo([FromQuery] int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeletePaymentInfoCommand()
                {
                    idPaymentInfo = id
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}

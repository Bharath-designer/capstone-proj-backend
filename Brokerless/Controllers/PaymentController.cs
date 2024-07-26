using System.Diagnostics.CodeAnalysis;
using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.Payment;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brokerless.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService) {
            _paymentService = paymentService;
        }


        [HttpPost]
        [Route("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymentWebHook([FromBody] PaymentNotificationDTO paymentNotificationDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    var customErrorResponse = new ErrorApiResponse
                    {
                        ErrCode = 1001,
                        Message = "One or more validation errors occurred.",
                        Error = errors
                    };


                    return BadRequest(customErrorResponse);
                }
                var headers = HttpContext.Request.Headers;

                await _paymentService.UpdateTransactionDetails(paymentNotificationDTO, headers);

                return Ok("Payment has been Updated");
            }
            catch (NoTransactionFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 500,
                    Message = "Internal Server Error"
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorObject);
            }
        }

    }
}

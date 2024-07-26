using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.Payment;
using Brokerless.DTOs.Subscription;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Brokerless.Controllers
{
    [ApiController]
    [Route("/api/v1/subscription")]
    public class SubscriptionController: ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService) {
            _subscriptionService = subscriptionService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubscription(UpdateSubscriptionDTO updateSubscriptionDTO)
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

                int UserId = int.Parse(User.FindFirst("userId").Value.ToString());


                MakePaymentReturnDTO makePaymentReturnDTO =  await _subscriptionService.UpdateSubscription(UserId, updateSubscriptionDTO.SubscriptionName);

                return Ok(makePaymentReturnDTO);
            }
            catch(PlanIsActiveException ex)
            {
                return BadRequest(new ErrorApiResponse
                {
                    ErrCode = 1004,
                    Message = ex.Message,
                });
            }
            catch(InvalidSubscriptionName ex)
            {
                return BadRequest(new ErrorApiResponse
                {
                    ErrCode = 1005,
                    Message = ex.Message,
                });
            }
            catch (FreeSubscriptionIsUsedException ex)
            {
                return BadRequest(new ErrorApiResponse
                {
                    ErrCode = 1006,
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}

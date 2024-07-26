using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.User;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Brokerless.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brokerless.Controllers
{

    [ApiController]
    [Route("/api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [Route("phone")]
        public async Task<IActionResult> UpdateUserMobileNumber([FromBody] UserMobileNumberUpdateDTO userMobileNumberUpdateDTO)
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

                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                await _userService.UpdateUserMobileNumber(userId, userMobileNumberUpdateDTO);

                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1009,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 500,
                    Message = "Internal Server Error"
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorObject);

            }
        }

        [HttpPut]
        [Route("verify")]
        public async Task<IActionResult> VerifyMobileNumber([FromBody] OtpDTO otpDTO)
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

                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                await _userService.VerifyMobileNumber(userId, otpDTO);

                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1009,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch(InvalidOTPException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1010,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 500,
                    Message = "Internal Server Error"
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorObject);

            }
        }

        [HttpPost]
        [Route("conversation")]
        public async Task<IActionResult> CreateConversation(CreateConversationDTO createConversationDTO)
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

                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                await _userService.CreateConversationWithPropertyOwner(userId, createConversationDTO);

                return Ok();
            }
            catch(PropertyDetailsNotRequestedException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1012,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
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

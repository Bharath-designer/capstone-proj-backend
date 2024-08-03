using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.Auth;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brokerless.Controllers
{
    [ApiController]
    [Route("/api/v1/auth/")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost]
        [Route("google")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateWithGoogle([FromBody]GoogleLoginDTO googleLoginDTO)
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

                AuthReturnDTO authReturnDTO =  await _authService.AuthenticateWithGoogle(googleLoginDTO.Token);

                return Ok(authReturnDTO);
            }
            catch(InvalidJwtException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1002,
                    Message = "Invalid Google Auth Token"
                };
                return BadRequest(errorObject);
            }
            catch(GmailNotVerifiedException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1003,
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

        [HttpGet]
        [Route("verify")]
        public async Task<IActionResult> VerifyUser()
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("userId").Value.ToString());

                var returnDTO = await _authService.GetVerifyDetails(UserId);
                return Ok(returnDTO);
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
    }

}

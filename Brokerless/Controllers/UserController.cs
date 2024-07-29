using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.User;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Brokerless.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brokerless.Controllers
{

    [ApiController]
    [Route("/api/v1/user")]
    [Authorize(Roles = "User")]
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
            catch (MobileNumberNotAdded ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1017,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (InvalidOTPException ex)
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

        [HttpGet]
        [Route("conversation")]
        public async Task<IActionResult> GetAllConversation([FromQuery] int pageNumber)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                List<ConversationListReturnDTO> conversations = await _userService.GetAllConversationForAUser(userId, pageNumber);

                return Ok(conversations);
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

                var data = await _userService.CreateConversationWithPropertyOwner(userId, createConversationDTO);

                return Ok(data);
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


        [HttpGet]
        [Route("message/{conversationId}")]
        public async Task<IActionResult> GetAllMessageForAConversation(int conversationId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                ChatMessagesReturnDTO messages = await _userService.GetAllMessageForAConversation(userId, conversationId);

                return Ok(messages);
            }
            catch (ConversationNotFoundException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1016,
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
        [Route("message")]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO createMessageDTO)
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

                await _userService.CreateMessageForConversation(userId, createMessageDTO);

                return Ok();
            }
            catch (ConversationNotFoundException ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1016,
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
        [Route("profile")]
        public async Task<IActionResult> GetProfileDetails()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                ProfileDetailsDTO profileDetailsDTO = await _userService.GetUserProfileDetails(userId);

                return Ok(profileDetailsDTO);
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

        [HttpGet]
        [Route("listings")]
        public async Task<IActionResult> GetUserListings()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                var myListings = await _userService.GetUserListings(userId);

                return Ok(myListings);
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
        [Route("listings/{propertyId}")]
        public async Task<IActionResult> GetUserListedPropertyDetails(int propertyId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());


                var myPropertyDetails = await _userService.GetUserListedPropertyDetails(userId, propertyId);

                return Ok(myPropertyDetails);
            }
            catch (PropertyNotFound ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
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
        [Route("listings/users/{propertyId}")]
        public async Task<IActionResult> GetListedPropertyRequestUsers(int propertyId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());

                var myPropertyDetails = await _userService.GetViewedUsersOfProperty(userId, propertyId);

                return Ok(myPropertyDetails);
            }
            catch (PropertyNotFound ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
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
        [Route("seller/conversation")]
        public async Task<IActionResult> CreateConversationWithViewer(CreateConversationWithRequestedUserDTO createConversationWithRequestedUserDTO)
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

                var data = await _userService.CreateConversationWithPropertyRequestedUser(userId, createConversationWithRequestedUserDTO);

                return Ok(data);
            }
            catch (UserNotRequestedForYourProperty ex)
            {
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1018,
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
        [Route("property/requested")]
        public async Task<IActionResult> GetUserListedPropertyDetails2()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value.ToString());


                var myRequestedPropertiesDetails = await _userService.GetUserRequestedPropertyDetails(userId);

                return Ok(myRequestedPropertiesDetails);
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

using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.Property;
using Microsoft.AspNetCore.Mvc;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Brokerless.Controllers
{

    [ApiController]
    [Route("/api/v1/property")]
    [Authorize(Roles = "User")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProperties([FromQuery] PropertySearchFilterDTO propertySearchFilterDTO)
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

                int? userId = User.FindFirst("userId") != null ? int.Parse(User.FindFirst("userId").Value.ToString()) : null;
                List<PropertyReturnDTO> data = await _propertyService.GetPropertiesWithFilters(userId, propertySearchFilterDTO);
                return Ok(data);
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
        [Route("{propertyId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetails([FromRoute] int propertyId)
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

                int? userId = User.FindFirst("userId") != null ? int.Parse(User.FindFirst("userId").Value.ToString()) : null;

                PropertyReturnDTO data = await _propertyService.GetPropertyByIdForUser(userId, propertyId);
                return Ok(data);
            }
            catch (PropertyNotFound ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
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
        [Route("request")]
        public async Task<IActionResult> GetRequestForAProperty(RequestPropertyDTO requestPropertyDTO)
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

                await _propertyService.RequestForProperty(userId, requestPropertyDTO);

                return Ok();
            }
            catch (PropertyViewingLimitExceededException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1013,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

            }
            catch (MobileNotVerifiedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1008,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (PropertyDetailsRequestAlreadySatisfied ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1014,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (UserNotFoundException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1009,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (OwnPropertyRequestedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1015,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (PropertyNotFound ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1011,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
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
        [Route("house")]
        public async Task<IActionResult> CreateHouseProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] HouseDetailsDTO basePropertyDTO
            )
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

                await _propertyService.CreateProperty(UserId, basePropertyDTO, files);

                return Ok();
            }
            catch (MobileNotVerifiedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1008,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (PropertyPostingLimitExceededException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1007,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("hostel")]
        public async Task<IActionResult> CreateHostelProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] HostelDetailsDTO basePropertyDTO
            )
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

                await _propertyService.CreateProperty(UserId, basePropertyDTO, files);

                return Ok();
            }
            catch (MobileNotVerifiedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1008,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (PropertyPostingLimitExceededException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1007,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("commercial")]
        public async Task<IActionResult> CreateCommercialProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] CommercialDetailsDTO basePropertyDTO
            )
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

                await _propertyService.CreateProperty(UserId, basePropertyDTO, files);

                return Ok();
            }
            catch (MobileNotVerifiedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1008,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (PropertyPostingLimitExceededException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1007,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("product")]
        public async Task<IActionResult> CreateProductProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] ProductDetailsDTO basePropertyDTO
            )
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

                await _propertyService.CreateProperty(UserId, basePropertyDTO, files);

                return Ok();
            }
            catch (MobileNotVerifiedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1008,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (PropertyPostingLimitExceededException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1007,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("land")]
        public async Task<IActionResult> CreateLandProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] LandDetailsDTO basePropertyDTO
            )
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

                await _propertyService.CreateProperty(UserId, basePropertyDTO, files);

                return Ok();
            }
            catch (MobileNotVerifiedException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1008,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (PropertyPostingLimitExceededException ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1007,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("house")]
        public async Task<IActionResult> UpdateHouseProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] UpdateHouseDetailsDTO updateHouseDetailsDTO
            )
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

                await _propertyService.UpdateProperty(UserId, updateHouseDetailsDTO, files);

                return Ok();
            }
            catch (PropertyNotFound ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("hostel")]
        public async Task<IActionResult> UpdateHostelProperty(
           [FromForm] List<IFormFile> files,
           [FromForm] UpdateHostelDetails updateHostelDetails
           )
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

                await _propertyService.UpdateProperty(UserId, updateHostelDetails, files);

                return Ok();
            }
            catch (PropertyNotFound ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("commercial")]
        public async Task<IActionResult> UpdateCommercialProperty(
           [FromForm] List<IFormFile> files,
           [FromForm] UpdateCommercialDetailsDTO updateCommercialDetailsDTO
           )
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

                await _propertyService.UpdateProperty(UserId, updateCommercialDetailsDTO, files);

                return Ok();
            }
            catch (PropertyNotFound ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("product")]
        public async Task<IActionResult> UpdateProductProperty(
            [FromForm] List<IFormFile> files,
            [FromForm] UpdateProductDetailsDTO updateProductDetailsDTO
            )
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

                await _propertyService.UpdateProperty(UserId, updateProductDetailsDTO, files);

                return Ok();
            }
            catch (PropertyNotFound ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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
        [Route("land")]
        public async Task<IActionResult> UpdateLandProperty(
           [FromForm] List<IFormFile> files,
           [FromForm] UpdateLandDetailsDTO updateLandDetailsDTO
           )
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

                await _propertyService.UpdateProperty(UserId, updateLandDetailsDTO, files);

                return Ok();
            }
            catch (PropertyNotFound ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1011,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (FileRuleException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                var errorObject = new ErrorApiResponse
                {
                    ErrCode = 1019,
                    Message = ex.Message
                };

                return BadRequest(errorObject);
            }
            catch (CustomModelFieldError ex)
            {
                var customErrorResponse = new ErrorApiResponse
                {
                    ErrCode = 1001,
                    Message = ex.Message
                };

                return BadRequest(customErrorResponse);

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

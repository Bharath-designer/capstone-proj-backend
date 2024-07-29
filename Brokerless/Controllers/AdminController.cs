using Brokerless.DTOs.Admin;
using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.Property;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Brokerless.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brokerless.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("/api/v1/admin")]
    public class AdminController: ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("property")]
        [HttpPut]
        public async Task<IActionResult> TogglePropertyApproval(PropertyApprovalToggleDTO propertyApprovalToggleDTO)
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

                    var returnDTO = await _adminService.TogglePropertyStatus(propertyApprovalToggleDTO);

                    return Ok(returnDTO);
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

        [HttpGet]
        [Route("property")]
        public async Task<IActionResult> GetAllProperties([FromQuery] AdminPropertySearchFilter adminPropertySearchFilterDTO)
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

                List<PropertyReturnDTO> data = await _adminService.GetAdminPropertiesWithFilters(adminPropertySearchFilterDTO);
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
        [Route("property/{propertyId}")]
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


                PropertyReturnDTO data = await _adminService.GetAdminPropertyById(propertyId);
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

    }
}
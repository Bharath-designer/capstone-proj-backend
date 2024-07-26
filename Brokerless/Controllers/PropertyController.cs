using Brokerless.DTOs.ApiResponse;
using Brokerless.DTOs.Property;
using Microsoft.AspNetCore.Mvc;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Services;
using Brokerless.Utilities;

namespace Brokerless.Controllers
{

    [ApiController]
    [Route("/api/v1/property")]
    public class PropertyController: ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService) {
            _propertyService = propertyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(
            [FromForm]List<IFormFile> files,
            [FromBody] BasePropertyDTO basePropertyDTO
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

                //IFormCollection requestForm =  Request.Form;
                //int UserId = int.Parse(User.FindFirst("userId").Value.ToString());

                //await _propertyService.CreateProperty(UserId, requestForm, basePropertyDTO, files);


                if (basePropertyDTO is ProductDetailsDTO childA)
                {
                    // Handle ChildDtoA
                    await Console.Out.WriteLineAsync("Product");
                }
                else if (basePropertyDTO is LandDetailsDTO childB)
                {
                    // Handle ChildDtoB
                    await Console.Out.WriteLineAsync("landddd");

                }

                return Ok();



                return Ok();
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
            catch   (CustomModelFieldError ex)
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


    }
}

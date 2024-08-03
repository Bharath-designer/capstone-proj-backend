using Brokerless.DTOs.ApiResponse;
using Brokerless.Interfaces.Services;
using Brokerless.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brokerless.Controllers
{
    [ApiController]
    [Route("/api/v1/tags")]
    [AllowAnonymous]
    public class TagController:ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService) {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags([FromQuery] string? query)
        {
            try {

                var tags = await _tagService.GetTagsWithQueryString(query);

                return Ok(tags);
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

using Microsoft.AspNetCore.Mvc;
using AdPlatform.Services;
namespace AdPlatform.controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PlatformsController : ControllerBase
    {
        private readonly IAdService _adService;
        public PlatformsController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpPost("load")]
        public IActionResult LoadPlatforms(IFormFile file)
        {
            try
            { 
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Файл пустой или не выбран.");
                    
                    
                }
                _adService.LoadPlatformsFromFile(file);
                return Ok(new { message = "Платофрмы успешно загружены" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при загрузке файла: {ex.Message}");
            }
        }

        [HttpGet("find")]
        public IActionResult FindPlatforms([FromQuery] string location)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest(new { error = "локация не указана" });

                }
                var platforms = _adService.FindPlatformsForLocation(location);
                return Ok(new { location = location, platforms = platforms });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}


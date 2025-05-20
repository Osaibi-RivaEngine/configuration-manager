using ConfigManagerApp.API.Repos;

using Microsoft.AspNetCore.Mvc;
using static ConfigManagerApp.API.Repos.ConfigRepo;

namespace ConfigManagerApp.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ConfigsController : ControllerBase
    {

        private readonly ILogger<ConfigsController> _logger;
        private readonly IConfigRepo _configRepo;
        public ConfigsController(ILogger<ConfigsController> logger, IConfigRepo configRepo)
        {
            _logger = logger;
            _configRepo = configRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_configRepo.GetAll());
        }

        [HttpPost]
        public ActionResult Add([FromBody] string keyValuePair)
        {
            try
            {
                var model = _configRepo.Add(keyValuePair);
                return Created("api/Configs/Add", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding config: { ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                _configRepo.Delete();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting config: { ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Filter(string filter)
        {
            try
            {
                return Ok(_configRepo.Filter(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error filtering config: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Sort(int type)
        {
            try{
                return Ok(_configRepo.Sort((SortType)type));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sorting config: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
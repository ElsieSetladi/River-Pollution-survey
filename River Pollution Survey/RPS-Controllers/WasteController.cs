using Microsoft.AspNetCore.Mvc;
using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;
using System.Net;

namespace River_Pollution_Survey.RPS_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WasteController : ControllerBase
    {
        private readonly ILogger<WasteController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public WasteController(ILogger<WasteController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWaste([FromBody] Waste model)
        {
            if (model == null)
            {
                return BadRequest("Please enter details!");
            }
            try
            {
                var waste = new Waste
                {
                    WasteId = Guid.NewGuid(),
                    Quantity = model.Quantity,
                    WasteType = model.WasteType,
                    Location = model.Location,
                    Image = model.Image

                };
                await _repositoryWrapper.WasteRepository.CreateAsync(waste);
                await _repositoryWrapper.WasteRepository.SaveAsync();
                return Ok(_repositoryWrapper.WasteRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");
            }


        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var wastes = await _repositoryWrapper.WasteRepository.GetAllAsync();
                return Ok(wastes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var wastes = await _repositoryWrapper.WasteRepository.FindByIdAsync(x => x.WasteId == id);

                if (wastes == null)
                {
                    return NotFound();
                }
                return Ok(wastes);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");

            }
        }

        

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteById(Guid id)
        {
            try
            {

                var toDelete = await _repositoryWrapper.WasteRepository.FindByIdAsync(x => x.WasteId == id);
                if (toDelete == null)
                {
                    return NotFound($"No Waste was found.");
                }


                await _repositoryWrapper.WasteRepository.DeleteAsync(toDelete);
                await _repositoryWrapper.WasteRepository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


    }
}

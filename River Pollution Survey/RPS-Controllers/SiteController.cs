using Microsoft.AspNetCore.Mvc;
using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;
using System.Net;

namespace River_Pollution_Survey.RPS_Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public SiteController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllAsync()
        {
            var sites = await _repositoryWrapper.SiteRepository.GetAllAsync();
            return Ok(sites);
        }


        [HttpGet]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var sites = await _repositoryWrapper.SiteRepository.FindByIdAsync(x => x.SiteId == id);
            if (sites == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return Ok(sites);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSite([FromBody] Site model)
        {
            if (model == null)
            {
                return BadRequest("Please enter data");
            }
            try
            {
                var site = new Site
                {
                    SiteId = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Location = model.Location,
                    Action = model.Action,
                    Recommendation = model.Recommendation,
                    

                };
                await _repositoryWrapper.SiteRepository.CreateAsync(site);
                await _repositoryWrapper.SiteRepository.SaveAsync();
                return Ok(_repositoryWrapper.SiteRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");
            }


        }


        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync([FromBody] Site site, Guid id)
        {

            var toUpdate = await _repositoryWrapper.SiteRepository.FindByIdAsync(x => x.SiteId == id);
            if (toUpdate == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            else
            {
                if (!String.IsNullOrEmpty(toUpdate.Name))
                {
                    toUpdate.Name = site.Name;
                    toUpdate.Location = site.Location;
                    toUpdate.Action = site.Action;
                    toUpdate.Recommendation = site.Recommendation;
                    toUpdate.Image = site.Image;

                }

                await _repositoryWrapper.SiteRepository.UpdateAsync(toUpdate);
                await _repositoryWrapper.SiteRepository.SaveAsync();
                return Ok(_repositoryWrapper.SiteRepository.GetAllAsync());
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var toDelete = await _repositoryWrapper.SiteRepository.FindByIdAsync(x => x.SiteId == id);
            if (toDelete == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            else
            {
                await _repositoryWrapper.SiteRepository.DeleteAsync(toDelete);
                await _repositoryWrapper.SiteRepository.SaveAsync();
                return Ok(_repositoryWrapper.SiteRepository.GetAllAsync());
            }
        }
    }
}

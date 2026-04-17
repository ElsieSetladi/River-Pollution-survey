using Microsoft.AspNetCore.Mvc;
using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;
using System.Net;

namespace River_Pollution_Survey.RPS_Controllers
{
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public RoleController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        [HttpGet]
        public async Task<IActionResult> ListAllRolesAsync()
        {
            var roles = await _repositoryWrapper.RoleRepository.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IActionResult> FindByIdAsync(Guid roleId)
        {
            var role = await _repositoryWrapper.RoleRepository.FindByIdAsync(x => x.RoleId == roleId);
            if (role == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] Role model)
        {
            model.RoleId = Guid.NewGuid();
            await _repositoryWrapper.RoleRepository.CreateAsync(model);
            await _repositoryWrapper.RoleRepository.SaveAsync();
            return Ok(_repositoryWrapper.RoleRepository.GetAllAsync());
        }
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync([FromBody] Role model, Guid roleId)
        {
            var toUpdate = await _repositoryWrapper.RoleRepository.FindByIdAsync(x => x.RoleId == roleId);
            if (toUpdate == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            else
            {
                if (!String.IsNullOrEmpty(toUpdate.Description))
                {
                    toUpdate.Description = model.Description;

                }

                await _repositoryWrapper.RoleRepository.UpdateAsync(toUpdate);
                await _repositoryWrapper.RoleRepository.SaveAsync();
                return Ok(_repositoryWrapper.RoleRepository.GetAllAsync());
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid roleId)
        {
            var toDelete = await _repositoryWrapper.RoleRepository.FindByIdAsync(x => x.RoleId == roleId);
            if (toDelete == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            else
            {
                await _repositoryWrapper.RoleRepository.DeleteAsync(toDelete);
                await _repositoryWrapper.RoleRepository.SaveAsync();
                return Ok(_repositoryWrapper.RoleRepository.GetAllAsync());
            }
        }
    }
}

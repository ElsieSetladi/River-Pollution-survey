using Microsoft.AspNetCore.Mvc;
using River_Pollution_Survey.Models.ViewModels;
using River_Pollution_Survey.RPS_Contracts;
using System.Net;

namespace River_Pollution_Survey.RPS_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserController(ILogger<UserController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserViewModel model)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var userExists = await _repositoryWrapper.UserRepository.FindByConditionAsync(x => x.EmailAddress.ToLower() == model.EmailAddress.ToLower()
                    || x.Contact == model.Contact);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (userExists == null)
                {
                    var user = new River_Pollution_Survey.Models.DBModels.User()
                    {
                        EmailAddress = model.EmailAddress,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Contact = model.Contact
                        

                    };
                    await _repositoryWrapper.UserRepository.CreateAsync(user);
                    await _repositoryWrapper.UserRepository.SaveAsync();

                    return Ok();

                }
                return NotFound("Email address or phone number is already registered");

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
                var users = await _repositoryWrapper.UserRepository.GetAllAsync();
                return Ok(users);
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
                var user = await _repositoryWrapper.UserRepository.FindByIdAsync(x => x.UserId == id);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateById(Guid id, [FromBody] UserViewModel model)
        {
            if (model == null)
            {
                return BadRequest("No details were found!");
            }

            try
            {
                var existingUser = await _repositoryWrapper.UserRepository.FindByIdAsync(x => x.UserId == id);
                if (existingUser == null)
                {
                    return NotFound($"User not found.");
                }


                existingUser.FirstName = model.FirstName;
                existingUser.LastName = model.LastName;
                existingUser.Contact = model.Contact;
                existingUser.EmailAddress = model.EmailAddress;
                

                await _repositoryWrapper.UserRepository.UpdateAsync(existingUser);
                await _repositoryWrapper.UserRepository.SaveAsync();


                var updatedUserViewModel = new UpdateUserViewModel
                {
                    UserId = existingUser.UserId,
                    LastName = existingUser.LastName,
                    FirstName = existingUser.FirstName,
                    Contact = existingUser.Contact,
                    EmailAddress = existingUser.EmailAddress,
                    
                };

                return Ok(updatedUserViewModel);
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
                
                var toDelete = await _repositoryWrapper.UserRepository.FindByIdAsync(x => x.UserId == id);
                if (toDelete == null)
                {
                    return NotFound($"User not found.");
                }

               
                await _repositoryWrapper.UserRepository.DeleteAsync(toDelete);
                await _repositoryWrapper.UserRepository.SaveAsync();

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

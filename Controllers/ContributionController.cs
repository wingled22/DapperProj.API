using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj.API.Models;
using Proj.API.Repositories;

namespace Proj.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private ContributionRepository _contributionRepository;
        public ContributionController(ContributionRepository contributionRepository)
        {
            _contributionRepository = contributionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetContributions(int clientId)
        {
            try{
                var contributions  =  await _contributionRepository.GetContributions(clientId);
                return Ok(contributions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

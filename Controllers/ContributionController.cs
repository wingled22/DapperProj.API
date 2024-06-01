using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj.API.Models;
using Proj.API.Repositories;

namespace Proj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private ContributionRepository _contributionRepository;
        public ContributionController(ContributionRepository contributionRepository)
        {
            _contributionRepository = contributionRepository;
        }

        [HttpGet("client/{clientId}")]
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

        [HttpPost]
        public async Task<ActionResult<Contribution>> PostContribution(Contribution contribution)
        {
            var result = await _contributionRepository.SaveContribution(contribution);
            if (result == null)
                return StatusCode(500, "Something happened try again later");

            return Ok(result);
        }
    }
}

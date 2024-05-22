using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proj.API.Models;
using Proj.API.Repositories;

namespace Proj.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;
        private readonly ContributionRepository _contributionRepository;

        public ClientController(ClientRepository clientRepository, ContributionRepository contributionRepository)
        {
            _clientRepository = clientRepository;
            _contributionRepository = contributionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _clientRepository.GetClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientById(id);
                if (client == null)
                    return NotFound();

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("contribution")]
        public async Task<IActionResult> GetClientsContributionSummary(int clientId){
            try{
                if (clientId == 0)
                    return BadRequest("Invalid clientId");
                var contributions = await _contributionRepository.GetClientContributionSummary(clientId);
                return Ok(contributions);
            }
            catch(Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest("Client object is null");

                if (!ModelState.IsValid)
                    return BadRequest("Invalid model object");

                var insertedClient = await _clientRepository.CreateClient(client);
                return CreatedAtAction(nameof(GetClientById), new { id = insertedClient.Id }, insertedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest("Client object is null");

                if (!ModelState.IsValid)
                    return BadRequest("Invalid model object");

                if (id != client.Id)
                    return BadRequest("Client ID mismatch");

                var result = await _clientRepository.UpdateClient(client);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var isDeleted = await _clientRepository.DeleteClient(id);
                if (!isDeleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }    
    
}
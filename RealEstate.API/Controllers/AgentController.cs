using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Service;
using RealEstate.Application.Service.Interfaces;
using RealEstate.Structure.Validators;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> _logger;
        private readonly IAgentService _agentService;
        private readonly IValidator<AgentRequestDto> _agentRequestValidatorDto;

        public AgentController(ILogger<AgentController> logger, IAgentService agentService, IValidator<AgentRequestDto> agentRequestValidatorDto)
        {
            _logger = logger;
            _agentService = agentService;
            _agentRequestValidatorDto = agentRequestValidatorDto;
        }

        /// <summary>
        /// 
        /// Https Get Method to gather a List of all Agents
        /// 
        /// </summary>
        /// 
        /// Sample Request:
        /// 
        ///     GET /api/Agent
        /// 
        /// <returns> List<Agent> </returns>
        [HttpGet(Name = "GetAllAgents")]
        public async Task<ActionResult<List<Agent>>> GetAllAgentsAsync()
        {
            try
            {
                var agents = await _agentService.GetAllAgentsAsync();
                return agents.IsSuccess ? Ok(agents.Result) : Problem(agents.ProblemType, agents.AdditionalInformation.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving agents.");
                throw;
            }
        }

        /// <summary>
        /// 
        /// Https Get Method to an Agent by Id
        /// 
        /// </summary>
        /// 
        /// Sample Request:
        /// 
        ///     GET /api/Agent/{agentId}
        /// 
        /// <returns> Agent </returns>
        [HttpGet("{agentId}", Name = "GetAgentById")]
        public async Task<ActionResult<Agent>> GetAgentByIdAsync(int agentId)
        {
            try
            {
                var agentbyId = await _agentService.GetAgentByIdAsync(agentId);
                return agentbyId.IsSuccess ? Ok(agentbyId.Result) : Problem(agentbyId.ProblemType, agentbyId.AdditionalInformation.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving agent.");
                throw;
            }
        }

        /// <summary>
        /// 
        /// Https Post Method to create a Agent
        /// 
        /// </summary>
        /// <param name="agentData"> Agent Data to be created </param>
        /// 
        /// Sample Request:
        /// 
        ///     POST /api/Agent
        ///     
        /// <returns> Agent </returns>
        [HttpPost(Name = "AddAgent")]
        public async Task<ActionResult<Agent>> AddAgentAsync(AgentRequestDto agentData)
        {
            var validationResult = _agentRequestValidatorDto.Validate(agentData);

            if (!validationResult.IsValid)
            {
                return new Agent();
            }

            var addAgent = await _agentService.AddAgentAsync(agentData);
            return addAgent.IsSuccess ? Ok(addAgent.Result) : Problem(addAgent.ProblemType, addAgent.AdditionalInformation.ToString());
        }

        [HttpDelete("{agentId}", Name = "DeleteAgentById")]
        public async Task<ActionResult<Agent>> DeleteAgentByIdAsync(int agentId)
        {
            var deleteAgent = await _agentService.DeleteAgentByIdAsync(agentId);
            return deleteAgent.IsSuccess ? Ok(deleteAgent.Result) : Problem(deleteAgent.ProblemType, string.Join(",", deleteAgent.AdditionalInformation));
        }
    }
}
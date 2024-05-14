using Microsoft.Extensions.Logging;
using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Repository.Interfaces;
using RealEstate.Application.Service.Interfaces;


namespace RealEstate.Application.Service
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;

        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        /// <summary>
        /// 
        /// Gather a List of all Agents
        /// 
        /// </summary>
        /// <returns> List<Agent> </returns>
        public async Task<ServiceResult<List<Agent>>> GetAllAgentsAsync()
        {
            return await _agentRepository.GetAllAgentsAsync();
        }

        /// <summary>
        /// 
        /// Gets a Agent by Id
        /// 
        /// </summary>
        /// <param name="agentId"> Id to get Agent </param>
        /// <returns> Agent </returns>
        public async Task<ServiceResult<Agent>> GetAgentByIdAsync(int agentId)
        {
            return await _agentRepository.GetAgentByIdAsync(agentId);
        }

        /// <summary>
        /// 
        /// Creates a Agent
        /// 
        /// </summary>
        /// <param name="agentData"> Data to be saved </param>
        /// <returns> Agent </returns>
        public async Task<ServiceResult<Agent>> AddAgentAsync(AgentRequestDto agentData) 
        {
            return await _agentRepository.AddAgentAsync(agentData);
        }

        /// <summary>
        /// 
        /// Deletes a Agent by Id
        /// 
        /// </summary>
        /// <param name="agentId"> Id to delete Agent </param>
        /// <returns> Agent </returns>
        public async Task<ServiceResult<Agent>> DeleteAgentByIdAsync(int agentId)
        {
            ServiceResult<Agent> response = new();

            response = await _agentRepository.DeleteAgentByIdAsync(agentId);

            if (!response.IsSuccess)
            {
                response.AdditionalInformation.Add($"Agent with ID {agentId} doesn't exist");
                return response;
            }

            return response;
        }

    }
}
using RealEstate.Domain;
using RealEstate.Structure.Dto.Request;

namespace RealEstate.Application.Service.Interfaces
{
    public interface IAgentService
    {
        /// <summary>
        /// 
        /// Gathers a List of all Agents
        /// 
        /// </summary>
        /// <returns> List<Agent> </returns>
        Task<ServiceResult<List<Agent>>> GetAllAgentsAsync();

        /// <summary>
        /// 
        /// Gets an Agent by Id
        /// 
        /// </summary>
        /// <param name="agentId"> Id to get an Agent </param>
        /// <returns> Agent </returns>
        Task<ServiceResult<Agent>> GetAgentByIdAsync(int agentId);

        /// <summary>
        /// 
        /// Save a Agent
        /// 
        /// </summary>
        /// <param name="agentData"> Agent Data to be Saved </param>
        /// <returns> Agent </returns>
        Task<ServiceResult<Agent>> AddAgentAsync(AgentRequestDto agentData);

        Task<ServiceResult<Agent>> DeleteAgentByIdAsync(int agentId);
    }
}
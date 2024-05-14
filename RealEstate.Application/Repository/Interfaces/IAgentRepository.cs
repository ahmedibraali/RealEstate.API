using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Service;

namespace RealEstate.Application.Repository.Interfaces
{
    public interface IAgentRepository
    {
        /// <summary>
        /// 
        /// Gather a List of all Agents
        /// 
        /// </summary>
        /// <returns> List<Agent> </returns>
        Task<ServiceResult<List<Agent>>> GetAllAgentsAsync();

        /// <summary>
        /// 
        /// Gets an Agent by Id
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// Deletes a Agent by Id
        /// 
        /// </summary>
        /// <param name="agentId"> Id to delete Agent </param>
        /// <returns> Agent </returns>
        Task<ServiceResult<Agent>> DeleteAgentByIdAsync(int agentId);
    }
}
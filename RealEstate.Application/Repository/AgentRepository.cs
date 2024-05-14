using Npgsql;
using NpgsqlTypes;
using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Repository.Interfaces;
using RealEstate.Application.Service;
using System.Xml.Linq;

namespace RealEstate.Application.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public AgentRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        /// <summary>
        /// 
        ///  Gets all the Agents from the Database
        /// 
        /// </summary>
        /// <returns> List<Agent> </returns>
        public async Task<ServiceResult<List<Agent>>> GetAllAgentsAsync()
        {
            List<Agent> agents = new List<Agent>();
            var result = new ServiceResult<List<Agent>>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var agentQuery = new NpgsqlCommand("SELECT * FROM agent;", conn);
                using var agentReader = await agentQuery.ExecuteReaderAsync();

                if (agentReader.HasRows) 
                {
                    while (await agentReader.ReadAsync())
                    {
                        var Agent = new Agent
                        {
                            Id = (int)agentReader["id"],
                            Name = (string)agentReader["name"],
                            PhoneNumber = (string)agentReader["phone_number"],
                            Email = (string)agentReader["email"],
                        };
                        agents.Add(Agent);
                    }
                }
                else
                {
                    result.AdditionalInformation.Add($"No data to retrieve");
                    return result;
                }

                result.IsSuccess = true;
                result.Result = agents;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.IsSuccess = false;
                result.AdditionalInformation.Add(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 
        /// Gets a Agent by Id from the Database
        /// 
        /// </summary>
        /// <param name="agentId"> Id to get Agent </param>
        /// <returns> Agent </returns>
        public async Task<ServiceResult<Agent>> GetAgentByIdAsync(int agentId)
        {
            Agent response = new();
            var result = new ServiceResult<Agent>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var agentQuery = new NpgsqlCommand("SELECT * FROM agent WHERE id = @agentId;", conn);
                agentQuery.Parameters.AddWithValue("@agentId", agentId);
                using var agentReader = await agentQuery.ExecuteReaderAsync();

                if (agentReader.HasRows)
                {
                    while (await agentReader.ReadAsync())
                    {
                        response = new Agent
                        {
                            Id = (int)agentReader["id"],
                            Name = (string)agentReader["name"],
                            PhoneNumber = (string)agentReader["phone_number"],
                            Email = (string)agentReader["email"],
                        };
                    }
                }
                else
                {
                    result.AdditionalInformation.Add($"Agent ID {agentId} doesn't exist");
                    return result;
                }

                result.IsSuccess = true;
                result.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.IsSuccess = false;
                result.AdditionalInformation.Add(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 
        /// Adds a Agent to the Database
        /// 
        /// </summary>
        /// <param name="agentData"> Agent Data to be created </param>
        /// <returns> Agent </returns>
        public async Task<ServiceResult<Agent>> AddAgentAsync(AgentRequestDto agentData) 
        { 
            Agent response = new();
            var serviceResult = new ServiceResult<Agent>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var query = new NpgsqlCommand("INSERT INTO agent (name, phone_number, email) Values (@agentName, @agentPhoneNumber, @agentEmail) returning id", conn);

                query.Parameters.Add(new NpgsqlParameter("@agentName", NpgsqlDbType.Text) { Value = agentData.Name });
                query.Parameters.Add(new NpgsqlParameter("@agentPhoneNumber", NpgsqlDbType.Text) { Value = agentData.Phone_Number });
                query.Parameters.Add(new NpgsqlParameter("@agentEmail", NpgsqlDbType.Text) { Value = agentData.Email });
                var result = await query.ExecuteScalarAsync();

                response = new Agent
                {
                    Id = (int)result,
                    Name = agentData.Name,
                    PhoneNumber = agentData.Phone_Number,
                    Email = agentData.Email,
                };

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;

            } 
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Deletes a Agent by Id from the Database
        /// 
        /// </summary>
        /// <param name="agentId"> Id to delete Agent </param>
        /// <returns> Agent </returns>
        public async Task<ServiceResult<Agent>> DeleteAgentByIdAsync(int agentId)
        {
            var serviceResult = new ServiceResult<Agent>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var delete = new NpgsqlCommand("DELETE FROM agent WHERE id = @AgentId", conn);
                delete.Parameters.AddWithValue("@AgentId", agentId);

                var result = await delete.ExecuteNonQueryAsync();

                if(result == 0) 
                {
                    serviceResult.AdditionalInformation.Add($"Deleted 0 Rows of agent");
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }
    }
}
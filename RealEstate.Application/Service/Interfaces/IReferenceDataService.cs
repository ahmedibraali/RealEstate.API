using RealEstate.Domain.Base;
using RealEstate.Structure.Dto.Request;
using RealEstate.Structure.Dto.Response;


namespace RealEstate.Application.Service.Interfaces
{
    public interface IReferenceDataService
    {
        /// <summary>
        /// 
        /// Gather a List of all Reference Data
        /// 
        /// </summary>
        /// <returns> ReferenceDataResponseDto </returns>
        Task<ServiceResult<ReferenceDataResponseDto>> GetAllReferenceDataAsync();

        /// <summary>
        /// 
        /// Save a Reference Data
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> AddReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData);

        /// <summary>
        /// 
        /// Delete Reference Data
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a Reference Data </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> DeleteReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Gets a Reference Data by Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to get Reference Data </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> GetReferenceDataByIdAsync(string refDataType, int refDataId);
    }
}
using RealEstate.Structure.Dto.Request;
using RealEstate.Structure.Dto.Response;
using RealEstate.Domain.Base;
using RealEstate.Application.Service;

namespace RealEstate.Application.Repository.Interfaces
{
    public interface IReferenceDataRepository
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
        /// Save a Typology
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> AddTypologyReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData);

        /// <summary>
        /// 
        /// Save a Real Estate Type
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> AddRealEstateTypeReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData);

        /// <summary>
        /// 
        /// Save a City
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> AddCityReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData);

        /// <summary>
        /// 
        /// Save a Amenity
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved</param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> AddAmenityReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData);

        /// <summary>
        /// 
        /// Deletes a Typology By Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a Typolgy </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> DeleteTypologyReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Deletes a Real Estate Type By Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a RealEstate Type </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> DeleteRealEstateTypeReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Deletes a City By Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> DeleteCityReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Deletes a Amenity By Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a Amenity </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> DeleteAmenityReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Gets a Typology by Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to get a Typology </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> GetTypologyReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Gets a City by Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to get a City </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> GetCityReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Gets a Real Estate Type by Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to get a Real Estate Type </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> GetRealEstateReferenceDataAsync(string refDataType, int refDataId);

        /// <summary>
        /// 
        /// Gets a Amenity by Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to get a Amenity </param>
        /// <returns> ReferenceData </returns>
        Task<ServiceResult<ReferenceData>> GetAmenityReferenceDataAsync(string refDataType, int refDataId);
    }
}
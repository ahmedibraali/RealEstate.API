using RealEstate.Structure.Dto.Request;
using RealEstate.Structure.Dto.Response;
using RealEstate.Domain.Base;
using RealEstate.Application.Repository.Interfaces;
using RealEstate.Application.Service.Interfaces;

namespace RealEstate.Application.Service
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IReferenceDataRepository _referenceDataRepository;

        public ReferenceDataService(IReferenceDataRepository referenceDataRepository)
        {
            _referenceDataRepository = referenceDataRepository;
        }

        //// <summary>
        /// 
        /// Gather a List of all Reference Data
        /// 
        /// </summary>
        /// <returns> ReferenceDataResponseDto </returns>
        public async Task<ServiceResult<ReferenceDataResponseDto>> GetAllReferenceDataAsync()
        {
            return await _referenceDataRepository.GetAllReferenceDataAsync();
        }

        /// <summary>
        /// 
        /// Creates a Reference Data
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> AddReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData)
        {
            ServiceResult<ReferenceData> response = new();

            if (!string.IsNullOrWhiteSpace(refDataType))
            {
                switch (refDataType.ToLower())
                {
                    case "typology":
                        response = await _referenceDataRepository.AddTypologyReferenceDataAsync(refDataType, refData);
                        break;

                    case "amenity":
                        response = await _referenceDataRepository.AddAmenityReferenceDataAsync(refDataType, refData);
                        break;

                    case "realestate_type":
                        response = await _referenceDataRepository.AddRealEstateTypeReferenceDataAsync(refDataType, refData);
                        break;

                    case "city":
                        response = await _referenceDataRepository.AddCityReferenceDataAsync(refDataType, refData);
                        break;
                }
            }
            
            return response;
        }

        /// <summary>
        /// 
        /// Delete Reference Data
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a Reference Data </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> DeleteReferenceDataAsync(string refDataType, int refDataId)
        {
            ServiceResult<ReferenceData> response = new();

            var existingReferenceData = await GetReferenceDataByIdAsync(refDataType, refDataId);

            if (existingReferenceData.Result == null)
            {
                response.IsSuccess = false;
                response.AdditionalInformation.Add($"Reference Data ID {refDataId} doesn't exist");
                return response;
            }

            switch (refDataType.ToLower())
            {
                case "typology":
                    response = await _referenceDataRepository.DeleteTypologyReferenceDataAsync(refDataType, refDataId);
                    break;

                case "realestate_type":
                    response = await _referenceDataRepository.DeleteRealEstateTypeReferenceDataAsync(refDataType, refDataId);
                    break;

                case "city":
                    response = await _referenceDataRepository.DeleteCityReferenceDataAsync(refDataType, refDataId);
                    break;

                case "amenity":
                    response = await _referenceDataRepository.DeleteAmenityReferenceDataAsync(refDataType, refDataId);
                    break;
            }

            return response;
        }

        /// <summary>
        /// 
        /// Gets a Reference Data by Id
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to get Reference Data </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> GetReferenceDataByIdAsync(string refDataType, int refDataId)
        {
            ServiceResult<ReferenceData> response = new();

            switch (refDataType.ToLower())
            {
                case "typology":
                    response = await _referenceDataRepository.GetTypologyReferenceDataAsync(refDataType, refDataId);
                    break;

                case "city":
                    response = await _referenceDataRepository.GetCityReferenceDataAsync(refDataType, refDataId);
                    break;

                case "realestate_type":
                    response = await _referenceDataRepository.GetRealEstateReferenceDataAsync(refDataType, refDataId);
                    break;

                case "amenity":
                    response = await _referenceDataRepository.GetAmenityReferenceDataAsync(refDataType, refDataId);
                    break;
            }

            return response;
        }
    }
}
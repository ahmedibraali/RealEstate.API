using Npgsql;
using NpgsqlTypes;
using RealEstate.Structure.Dto.Request;
using RealEstate.Domain.Base;
using RealEstate.Application.Repository.Interfaces;
using RealEstate.Application.Service;
using RealEstate.Structure.Dto.Response;
using RealEstate.Domain;

namespace RealEstate.Application.Repository
{
    public class ReferenceDataRepository : IReferenceDataRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public ReferenceDataRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        /// <summary>
        /// 
        /// Gets all Reference Data from the Database
        /// 
        /// </summary>
        /// <returns> ReferenceDataResponseDto </returns>
        public async Task<ServiceResult<ReferenceDataResponseDto>> GetAllReferenceDataAsync()
        {
            ReferenceDataResponseDto refData = new();
            var result = new ServiceResult<ReferenceDataResponseDto>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var typologyQuerry = new NpgsqlCommand("SELECT * FROM typology;", conn);
                using var typologyReader = await typologyQuerry.ExecuteReaderAsync();

                if (typologyReader.HasRows)
                {
                    while (typologyReader.Read())
                    {
                        var typology = new Typology
                        {
                            Description = typologyReader["description"].ToString(),
                            Id = (int)typologyReader["id"]
                        };
                        refData.Typologies.Add(typology);
                    }

                    typologyReader.Close();
                }
                else
                {
                    result.AdditionalInformation.Add($"No typology data to retrieve");
                    return result;
                }

                using var realEstateTypeQuerry = new NpgsqlCommand("SELECT * FROM realestate_type;", conn);
                var realEstateTypeReader = await realEstateTypeQuerry.ExecuteReaderAsync();

                if (realEstateTypeReader.HasRows)
                {
                    while (realEstateTypeReader.Read())
                    {
                        var RealEstateType = new RealEstateType
                        {
                            Description = realEstateTypeReader["description"].ToString(),
                            Id = (int)realEstateTypeReader["id"]
                        };
                        refData.RealEstateTypes.Add(RealEstateType);
                    }

                    realEstateTypeReader.Close();
                }
                else
                {
                    result.AdditionalInformation.Add($"No real estate type data to retrieve");
                    return result;
                }

                using var cityQuerry = new NpgsqlCommand("SELECT * FROM city;", conn);
                var cityReader = await cityQuerry.ExecuteReaderAsync();

                if (cityReader.HasRows)
                {
                    while (cityReader.Read())
                    {
                        var citiesModel = new City
                        {
                            Description = cityReader["description"].ToString(),
                            Id = (int)cityReader["id"]
                        };
                        refData.Cities.Add(citiesModel);
                    }

                    cityReader.Close();
                }
                else
                {
                    result.AdditionalInformation.Add($"No city data to retrieve");
                    return result;
                }

                using var amenitiesQuerry = new NpgsqlCommand("SELECT * FROM amenity;", conn);
                var amenitiesReader = await amenitiesQuerry.ExecuteReaderAsync();

                if (amenitiesReader.HasRows)
                {
                    while (amenitiesReader.Read())
                    {
                        var Amenities = new Amenities
                        {
                            Description = amenitiesReader["description"].ToString(),
                            Id = (int)amenitiesReader["id"]
                        };
                        refData.Amenities.Add(Amenities);
                    }

                    amenitiesReader.Close();
                }
                else
                {
                    result.AdditionalInformation.Add($"No amenity data to retrieve");
                    return result;
                }

                result.IsSuccess = true;
                result.Result = refData;
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
        /// Creates a Typology in the database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> AddTypologyReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var query = new NpgsqlCommand(@"INSERT INTO typology(description) values(@refDataDescription) returning id;", conn);

                query.Parameters.AddWithValue("@refDataDescription", NpgsqlDbType.Text, refData.Description);

                var result = await query.ExecuteScalarAsync();

                response = new ReferenceData
                {
                    Id = (int)result,
                    Description = refData.Description
                };

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Creates a Real Estate Type in the Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> AddRealEstateTypeReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var query = new NpgsqlCommand(@"INSERT INTO realestate_type(description) values(@refDataDescription) returning id;", conn);

                query.Parameters.AddWithValue("@refDataDescription", NpgsqlDbType.Text, refData.Description);

                var result = await query.ExecuteScalarAsync();

                response = new ReferenceData
                {
                    Id = (int)result,
                    Description = refData.Description
                };

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Creates a City in the Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> AddCityReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var query = new NpgsqlCommand(@"INSERT INTO city(description) values(@refDataDescription) returning id;", conn);

                query.Parameters.AddWithValue("@refDataDescription", NpgsqlDbType.Text, refData.Description);

                var result = await query.ExecuteScalarAsync();

                response = new ReferenceData
                {
                    Id = (int)result,
                    Description = refData.Description
                };

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Creates a Amenity in the Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refData"> Data to be saved </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> AddAmenityReferenceDataAsync(string refDataType, ReferenceDataRequestDto refData)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var query = new NpgsqlCommand(@"INSERT INTO amenity(description) values(@refDataDescription) returning id;", conn);

                query.Parameters.AddWithValue("@refDataDescription", NpgsqlDbType.Text, refData.Description);

                var result = await query.ExecuteScalarAsync();

                response = new ReferenceData
                {
                    Id = (int)result,
                    Description = refData.Description
                };

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Deletes a Typology By Id from the database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> DeleteTypologyReferenceDataAsync(string refDataType, int refDataId)
        {
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var delete = new NpgsqlCommand("DELETE FROM typology WHERE id = @RefDataId", conn);
                delete.Parameters.AddWithValue("@RefDataId", refDataId);

                var response = await GetTypologyReferenceDataAsync(refDataType, refDataId);
                var result = await delete.ExecuteScalarAsync();

                serviceResult.IsSuccess = true;
                serviceResult.Result = response.Result; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Deletes a Real Estate Type By Id from the Databse
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> DeleteRealEstateTypeReferenceDataAsync(string refDataType, int refDataId)
        {
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var delete = new NpgsqlCommand("DELETE FROM realestate_type WHERE id = @RefDataId", conn);
                delete.Parameters.AddWithValue("@RefDataId", refDataId);

                var response = await GetRealEstateReferenceDataAsync(refDataType, refDataId);
                var result = await delete.ExecuteScalarAsync();

                serviceResult.IsSuccess = true;
                serviceResult.Result = response.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Deletes a City By Id from the Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> DeleteCityReferenceDataAsync(string refDataType, int refDataId)
        {
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var delete = new NpgsqlCommand("DELETE FROM city WHERE id = @RefDataId", conn);
                delete.Parameters.AddWithValue("@RefDataId", refDataId);

                var response = await GetCityReferenceDataAsync(refDataType, refDataId);
                var result = await delete.ExecuteScalarAsync();

                serviceResult.IsSuccess = true;
                serviceResult.Result = response.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Deletes a Amenity By Id from the Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> DeleteAmenityReferenceDataAsync(string refDataType, int refDataId)
        {
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();
                using var delete = new NpgsqlCommand("DELETE FROM amenity WHERE id = @RefDataId", conn);
                delete.Parameters.AddWithValue("@RefDataId", refDataId);

                var response = await GetAmenityReferenceDataAsync(refDataType, refDataId);
                var result = await delete.ExecuteScalarAsync();             

                serviceResult.IsSuccess = true;
                serviceResult.Result = response.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Gets Typology by Id from Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> GetTypologyReferenceDataAsync(string refDataType, int refDataId)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var typologyQuery = new NpgsqlCommand("SELECT * FROM typology WHERE id = @RefDataId;", conn);
                typologyQuery.Parameters.AddWithValue("@RefDataId", refDataId);
                using var typologyReader = await typologyQuery.ExecuteReaderAsync();

                if (typologyReader.HasRows)
                {

                    while (typologyReader.Read())
                    {
                        response = new ReferenceData
                        {
                            Id = (int)typologyReader["id"],
                            Description = (string)typologyReader["description"],
                        };
                    }
                } else
                {
                    serviceResult.AdditionalInformation.Add($"Reference Data ID {refDataId} doesn't exist");
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Gets City by Id from Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> GetCityReferenceDataAsync(string refDataType, int refDataId)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var cityQuery = new NpgsqlCommand("SELECT * FROM city WHERE id = @RefDataId;", conn);
                cityQuery.Parameters.AddWithValue("@RefDataId", refDataId);
                using var cityReader = await cityQuery.ExecuteReaderAsync();

                if (cityReader.HasRows)
                {
                    while (cityReader.Read())
                    {
                        response = new ReferenceData
                        {
                            Id = (int)cityReader["id"],
                            Description = (string)cityReader["description"],
                        };
                    }
                } else
                {
                    serviceResult.AdditionalInformation.Add($"Reference Data ID {refDataId} doesn't exist");
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Gets Real Eatate Type by Id from Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> GetRealEstateReferenceDataAsync(string refDataType, int refDataId)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var realEstateQuery = new NpgsqlCommand("SELECT * FROM realestate_type WHERE id = @RefDataId;", conn);
                realEstateQuery.Parameters.AddWithValue("@RefDataId", refDataId);
                using var realEstateReader = await realEstateQuery.ExecuteReaderAsync();

                if (realEstateReader.HasRows)
                {
                    while (realEstateReader.Read())
                    {
                        response = new ReferenceData
                        {
                            Id = (int)realEstateReader["id"],
                            Description = (string)realEstateReader["description"],
                        };
                    }
                } else
                {
                    serviceResult.AdditionalInformation.Add($"Reference Data ID {refDataId} doesn't exist");
                    return serviceResult;
                }

                    serviceResult.IsSuccess = true;
                    serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }

        /// <summary>
        /// 
        /// Gets Amenity by Id from Database
        /// 
        /// </summary>
        /// <param name="refDataType"> Reference Data Type </param>
        /// <param name="refDataId"> Id to delete a City </param>
        /// <returns> ReferenceData </returns>
        public async Task<ServiceResult<ReferenceData>> GetAmenityReferenceDataAsync(string refDataType, int refDataId)
        {
            ReferenceData response = new();
            var serviceResult = new ServiceResult<ReferenceData>();

            try
            {
                using var conn = await _dataSource.OpenConnectionAsync();

                using var amenityQuery = new NpgsqlCommand("SELECT * FROM amenity WHERE id = @RefDataId;", conn);
                amenityQuery.Parameters.AddWithValue("@RefDataId", refDataId);
                using var amenityReader = await amenityQuery.ExecuteReaderAsync();

                if (amenityReader.HasRows)
                {
                    while (amenityReader.Read())
                    {
                        response = new ReferenceData
                        {
                            Id = (int)amenityReader["id"],
                            Description = (string)amenityReader["description"],
                        };
                    }
                } else
                {
                    serviceResult.AdditionalInformation.Add($"Reference Data ID {refDataId} doesn't exist");
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Result = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                serviceResult.IsSuccess = false;
                serviceResult.AdditionalInformation.Add(ex.Message);
            }

            return serviceResult;
        }
    }
}
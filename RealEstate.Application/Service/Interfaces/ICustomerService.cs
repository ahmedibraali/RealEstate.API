using RealEstate.Domain;
using RealEstate.Structure.Dto.Request;


namespace RealEstate.Application.Service.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// 
        /// Gathers a List of all Customer
        /// 
        /// </summary>
        /// <returns> List<Customer> </returns>
        Task<ServiceResult<List<Customer>>> GetAllCustomersAsync();

        /// <summary>
        /// 
        /// Save a Customer
        /// 
        /// </summary>
        /// <param name="customerData"> Customer Data to be Saved </param>
        /// <returns> Customer </returns>
        Task<ServiceResult<Customer>> AddCustomerAsync(CustomerRequestDto customerData);

        /// <summary>
        /// 
        /// Gets a Customer by Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// <returns> Customer </returns>
        Task<ServiceResult<Customer>> GetCustomerByIdAsync(int customerId);

        /// <summary>
        /// 
        /// Deletes a Customer By Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// <returns> Customer </returns>
        Task<ServiceResult<Customer>> DeleteCustomerByIdAsync(int customerId);
    }
}
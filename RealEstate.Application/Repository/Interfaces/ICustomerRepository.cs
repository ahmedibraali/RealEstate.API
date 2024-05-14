using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Service;

namespace RealEstate.Application.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// 
        /// Gather a List of all Customers
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
        /// Deletes a Customer by Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// <returns> Customer </returns>
        Task<ServiceResult<Customer>> DeleteCustomerByIdAsync(int customerId);
    }
}
using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Repository.Interfaces;
using RealEstate.Application.Service.Interfaces;

namespace RealEstate.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// 
        /// Gather a List of all Customers
        /// 
        /// </summary>
        /// <returns> List<Customer> </returns>
        public async Task<ServiceResult<List<Customer>>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        /// <summary>
        /// 
        /// Creates a Customer
        /// 
        /// </summary>
        /// <param name="customerData"></param>
        /// <returns> Customer </returns>
        public async Task<ServiceResult<Customer>> AddCustomerAsync(CustomerRequestDto customerData)
        {
            return await _customerRepository.AddCustomerAsync(customerData);
        }

        /// <summary>
        /// 
        /// Gets a Customer by Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// <returns> Customer </returns>
        public async Task<ServiceResult<Customer>> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }

        /// <summary>
        /// 
        /// Deletes a Customer by Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// <returns> Customer </returns>
        public async Task<ServiceResult<Customer>> DeleteCustomerByIdAsync(int customerId)
        {
            ServiceResult<Customer> response = new();

            var existCustomer = await GetCustomerByIdAsync(customerId);

            if (existCustomer.Result == null)
            {
                response.IsSuccess = false;
                response.AdditionalInformation.Add($"Cusomter with ID {customerId} doesn't exist");
                return response;
            }

            response = await _customerRepository.DeleteCustomerByIdAsync(customerId);

            return response;
        }
    }
}
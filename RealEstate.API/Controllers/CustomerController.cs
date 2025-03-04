﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Structure.Dto.Request;
using RealEstate.Domain;
using RealEstate.Application.Service.Interfaces;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerRequestDto> _customerRequestValidatorDto;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService, IValidator<CustomerRequestDto> customerRequestValidatorDto)
        {
            _logger = logger;
            _customerService = customerService;
            _customerRequestValidatorDto = customerRequestValidatorDto;
        }

        /// <summary>
        /// 
        /// Https Get Method to gather a List of all Customers
        /// 
        /// </summary>
        /// 
        /// Sample Request:
        /// 
        ///     GET /api/Customer
        /// 
        /// <returns> List<Customer> </returns>
        [HttpGet(Name = "GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return customers.IsSuccess ? Ok(customers.Result) : Problem(customers.ProblemType, customers.AdditionalInformation.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving customers.");
                throw;
            }
        }

        /// <summary>
        /// 
        /// Https Post Method to create a Customer
        /// 
        /// </summary>
        /// <param name="customerData"> Customer Data to be created </param>
        /// 
        /// Sample Request:
        /// 
        ///     POST /api/Customer
        ///     
        /// <returns> Customer </returns>
        [HttpPost(Name = "AddCustomer")]
        public async Task<ActionResult<Customer>> AddCustomerAsync(CustomerRequestDto customerData)
        {
            var validationResult = _customerRequestValidatorDto.Validate(customerData);

            if (!validationResult.IsValid)
            {
                return new Customer();
            }

            var addCustomer = await _customerService.AddCustomerAsync(customerData);
            return addCustomer.IsSuccess ? Ok(addCustomer.Result) : Problem(addCustomer.ProblemType, addCustomer.AdditionalInformation.ToString());
        }

        /// <summary>
        /// 
        /// Https Get Method to get a Customer by Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// 
        ///  Sample Request:
        /// 
        ///     GET api/Customer/{customerId}
        ///     
        /// <returns> Customer </returns>
        [HttpGet("{customerId}", Name = "GetCustomerById")]
        public async Task<ActionResult<Customer>> GetCustomerByIdAsync(int customerId)
        {
            var getCustomerById = await _customerService.GetCustomerByIdAsync(customerId);
            return getCustomerById.IsSuccess ? Ok(getCustomerById.Result) : Problem(getCustomerById.ProblemType, getCustomerById.AdditionalInformation.ToString());
        }

        /// <summary>
        /// 
        /// Https Delete Method to delete a Customer by Id
        /// 
        /// </summary>
        /// <param name="customerId"> Id to get Customer </param>
        /// 
        ///  Sample Request:
        /// 
        ///     DELETE api/Customer/{customerId}
        ///     
        /// <returns> Customer </returns>
        [HttpDelete("{customerId}", Name = "DeleteCustomerById")]
        public async Task<ActionResult<Customer>> DeleteCustomerByIdAsync(int customerId)
        {
            var deleteCustomer = await _customerService.DeleteCustomerByIdAsync(customerId);
            return deleteCustomer.IsSuccess ? Ok(deleteCustomer.Result) : Problem(deleteCustomer.ProblemType, string.Join(",", deleteCustomer.AdditionalInformation));
        }
    }
}
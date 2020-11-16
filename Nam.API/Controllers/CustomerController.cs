using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Categories.Dto;
using Nam.Application.Apps.Customers;
using Nam.Application.Apps.Customers.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService _customerService)
        {
            customerService = _customerService;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ResultBase> GetAll(RQParam input)
        {
            return await customerService.GetAll(input);
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<ResultBase> GetById(RQParamId input)
        {
            return await customerService.GetById(input);
        }

        [HttpPost]
        [Route("GetByPhone")]
        public async Task<ResultBase> GetByPhone(RQCustomerByPhone input)
        {
            return await customerService.GetByPhone(input);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ResultBase> Add(RQCustomer input)
        {
            return await customerService.Add(input);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ResultBase> Update(RQCustomer input)
        {
            return await customerService.Update(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await customerService.Delete(input);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Employees;
using Nam.Application.Apps.Employees.Dto;
using Nam.Application.Roles.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ResultBase> GetAll(RQParam input)
        {
            return await employeeService.GetAll(input);
        }

        [HttpPost]
        [Route("GetListByShopId")]
        public async Task<ResultBase> GetListByShopId(RQEmployeeByShopId input)
        {
            return await employeeService.GetListByShopId(input);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ResultBase> Add(RQEmployeeAdd input)
        {
            return await employeeService.Add(input);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ResultBase> Update(RQEmployeeAdd input)
        {
            return await employeeService.Update(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await employeeService.Delete(input);
        }
    }
}

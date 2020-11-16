using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Roles;
using Nam.Application.Roles.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RoleController(IRoleService _roleService)
        {
            roleService = _roleService;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ResultBase> GetAll(RQParam input)
        {
            return await roleService.GetAll(input);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ResultBase> Add(RQRoleAdd input)
        {
            return await roleService.Add(input);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ResultBase> Update(RQRoleAdd input)
        {
            return await roleService.Update(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await roleService.Delete(input);
        }
    }
}

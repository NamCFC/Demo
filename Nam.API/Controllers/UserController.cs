using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Customers;
using Nam.Application.Roles.Dto;
using Nam.Application.Users;
using Nam.Application.Users.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserController(IUserService _userService, IHttpContextAccessor _httpContextAccessor)
        {
            userService = _userService;
            httpContextAccessor = _httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ResultBase> Login(RQUserLogin input)
        {
            return await userService.Login(input);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ResultBase> Add(RQUserRegister input)
        {
            return await userService.Add(input);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ResultBase> ChangePassword(RQUserChangePassword input)
        {
            return await userService.ChangePassword(input);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await userService.Delete(input);
        }
    }
}

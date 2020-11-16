using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Shops;
using Nam.Application.Apps.Shops.Dto;
using Nam.Application.Roles.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService shopService;
        public ShopController(IShopService _shopService)
        {
            shopService = _shopService;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ResultBase> GetAll(RQParam input)
        {
            return await shopService.GetAll(input);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ResultBase> Add(RQShopAdd input)
        {
            return await shopService.Add(input);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ResultBase> Update(RQShopAdd input)
        {
            return await shopService.Update(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await shopService.Delete(input);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Products;
using Nam.Application.Apps.Products.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ResultBase> GetAll(RQParam input)
        {
            return await productService.GetAll(input);
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<ResultBase> GetById(RQParamId input)
        {
            return await productService.GetById(input);
        }

        [HttpPost]
        [Route("GetListByCategoryId")]
        public async Task<ResultBase> GetListByCategoryId(RQProductByCategoryId input)
        {
            return await productService.GetListByCategoryId(input);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ResultBase> Add(RQProductAdd input)
        {
            return await productService.Add(input);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ResultBase> Update(RQProductAdd input)
        {
            return await productService.Update(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await productService.Delete(input);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Bills;
using Nam.Application.Apps.Bills.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : ControllerBase
    {
        private readonly IBillService billService;
        public BillController(IBillService _billService)
        {
            billService = _billService;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ResultBase> GetAll(RQParam input)
        {
            return await billService.GetAll(input);
        }

        [HttpPost]
        [Route("GetListBillByCustomerId")]
        public async Task<ResultBase> GetListBillByCustomerId(RQBillByCustomerId input)
        {
            return await billService.GetListBillByCustomerId(input);
        }

        [HttpPost]
        [Route("GetListBillDetailByBillId")]
        public async Task<ResultBase> GetListBillDetailByBillId(RQBillDetailByBillId input)
        {
            return await billService.GetListBillDetailByBillId(input);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ResultBase> Add(RQBillAdd input)
        {
            return await billService.Add(input);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ResultBase> Update(RQBillAdd input)
        {
            return await billService.Update(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ResultBase> Delete(RQParamId input)
        {
            return await billService.Delete(input);
        }
    }
}

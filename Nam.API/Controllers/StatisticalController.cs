using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nam.Application.Apps.Statisticals;
using Nam.Application.Apps.Statisticals.Dto;
using Nam.Core.ResultBase;

namespace Nam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService statisticalService;
        public StatisticalController(IStatisticalService _statisticalService)
        {
            statisticalService = _statisticalService;
        }

        [HttpPost]
        [Route("StatisticByDay")]
        public async Task<ResultBase> StatisticByDay(RQStatisticByDay input)
        {
            return await statisticalService.StatisticByDay(input);
        }

        [HttpPost]
        [Route("StatisticByMonth")]
        public async Task<ResultBase> StatisticByMonth(RQStatisticByMonth input)
        {
            return await statisticalService.StatisticByMonth(input);
        }

        [HttpPost]
        [Route("StatisticByYear")]
        public async Task<ResultBase> StatisticByYear(RQStatisticByYear input)
        {
            return await statisticalService.StatisticByYear(input);
        }

        [HttpPost]
        [Route("StatisticByFromTo")]
        public async Task<ResultBase> StatisticByFromTo(RQStatisticByFromTo input)
        {
            return await statisticalService.StatisticByFromTo(input);
        }
    }
}

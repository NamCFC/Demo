using Nam.Application.Apps.Statisticals.Dto;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Statisticals
{
    public interface IStatisticalService
    {
        Task<ResultBase> StatisticByDay(RQStatisticByDay input);

        Task<ResultBase> StatisticByMonth(RQStatisticByMonth input);

        Task<ResultBase> StatisticByYear(RQStatisticByYear input);

        Task<ResultBase> StatisticByFromTo(RQStatisticByFromTo input);
    }
}

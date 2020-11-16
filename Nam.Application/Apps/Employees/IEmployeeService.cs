using Nam.Application.Apps.Employees.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Employees
{
    public interface IEmployeeService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> GetListByShopId(RQEmployeeByShopId input);

        Task<ResultBase> Add(RQEmployeeAdd input);

        Task<ResultBase> Update(RQEmployeeAdd input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

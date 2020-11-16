using Nam.Application.Apps.Bills.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Bills
{
    public interface IBillService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> GetListBillByCustomerId(RQBillByCustomerId input);

        Task<ResultBase> GetListBillDetailByBillId(RQBillDetailByBillId input);

        Task<ResultBase> Add(RQBillAdd input);

        Task<ResultBase> Update(RQBillAdd input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

using Nam.Application.Apps.Shops.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Shops
{
    public interface IShopService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> GetById(RQParamId input);

        Task<ResultBase> Add(RQShopAdd input);

        Task<ResultBase> Update(RQShopAdd input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

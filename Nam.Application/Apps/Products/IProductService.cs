using Nam.Application.Apps.Products.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Products
{
    public interface IProductService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> GetById(RQParamId input);

        Task<ResultBase> GetListByCategoryId(RQProductByCategoryId input);

        Task<ResultBase> Add(RQProductAdd input);

        Task<ResultBase> Update(RQProductAdd input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

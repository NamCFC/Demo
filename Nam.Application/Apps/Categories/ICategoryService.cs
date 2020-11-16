using Nam.Application.Apps.Categories.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Categories
{
    public interface ICategoryService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> Add(RQCategoryAdd input);

        Task<ResultBase> Update(RQCategoryAdd input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

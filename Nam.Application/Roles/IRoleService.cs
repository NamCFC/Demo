using Nam.Application.Roles.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Roles
{
    public interface IRoleService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> Add(RQRoleAdd input);

        Task<ResultBase> Update(RQRoleAdd input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

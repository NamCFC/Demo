using Nam.Application.Users.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Users
{
    public interface IUserService
    {
        Task<ResultBase> Login(RQUserLogin input);

        Task<ResultBase> Add(RQUserRegister input);

        Task<ResultBase> ChangePassword(RQUserChangePassword input);

        Task<ResultBase> LockAccount(RQParamId input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

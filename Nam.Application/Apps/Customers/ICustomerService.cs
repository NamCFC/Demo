using Nam.Application.Apps.Customers.Dto;
using Nam.Application.Users.Dto;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Customers
{
    public interface ICustomerService
    {
        Task<ResultBase> GetAll(RQParam input);

        Task<ResultBase> GetById(RQParamId input);

        Task<ResultBase> GetByPhone(RQCustomerByPhone input);

        Task<ResultBase> Add(RQCustomer input);

        Task<ResultBase> Update(RQCustomer input);

        Task<ResultBase> Delete(RQParamId input);
    }
}

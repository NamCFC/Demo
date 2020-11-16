using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Employees.Dto;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using Nam.Ultilities.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Employees
{
    public class EmployeeService : AppServiceBase, IEmployeeService
    {
        public EmployeeService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }


        //get all product
        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Employee", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await (from em in repo.GetAll<Employee>(u => u.IsDeleted == false)
                                  join us in repo.GetAll<User>(u => u.IsDeleted == false && u.IsActive == true) on em.UserId equals us.Id
                                  select new EmployeeDto
                                  {
                                      Id = em.Id,
                                      FullName = em.FullName,
                                      Gender = em.Gender,
                                      Address = em.Address,
                                      Birth = em.Birth,
                                      PhoneNumber = em.PhoneNumber,
                                      Email = em.Email,
                                      IdentityCardNumber = em.IdentityCardNumber
                                  }).ToListAsync();
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //get list employee by shop id
        public async Task<ResultBase> GetListByShopId(RQEmployeeByShopId input)
        {
            try
            {
                if (CheckExistSecCode("Employee", "GetListByShopId", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await (from em in repo.GetAll<Employee>(u => u.IsDeleted == false && u.ShopId == input.ShopId)
                                  join us in repo.GetAll<User>(u => u.IsDeleted == false && u.IsActive == true) on em.UserId equals us.Id
                                  select new EmployeeDto
                                  {
                                      Id = em.Id,
                                      FullName = em.FullName,
                                      Gender = em.Gender,
                                      Address = em.Address,
                                      Birth = em.Birth,
                                      PhoneNumber = em.PhoneNumber,
                                      Email = em.Email,
                                      IdentityCardNumber = em.IdentityCardNumber
                                  }).ToListAsync();
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        public async Task<ResultBase> Add(RQEmployeeAdd input)
        {
            try
            {
                if (CheckExistSecCode("Employee", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = Mappers.Mapper<RQEmployeeAdd, Employee>(input);
                await repo.AddAsync<Employee>(data);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        public async Task<ResultBase> Update(RQEmployeeAdd input)
        {
            try
            {
                if (CheckExistSecCode("Employee", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Employee>(input.Id);
                data.FullName = input.FullName;
                data.Gender = input.Gender;
                data.Address = input.Address;
                data.Birth = input.Birth;
                data.Email = input.Email;
                data.PhoneNumber = input.PhoneNumber;
                data.IdentityCardNumber = input.IdentityCardNumber;
                data.LastModifiedBy = GetUserId();
                data.LastModifiedDate = DateTime.Now;
                await repo.UpdateAsync<Employee>(data);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Employee", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Employee>(input.Id);
                data.IsDeleted = true;
                data.DeletedBy = GetUserId();
                data.DeletedDate = DateTime.Now;
                await repo.UpdateAsync<Employee>(data);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

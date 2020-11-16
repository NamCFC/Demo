using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Customers.Dto;
using Nam.Application.Users.Dto;
using Nam.Application.Authentication.JwtBearer;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using Nam.Ultilities.AutoMapper;
using Nam.Ultilities.Encrypts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nam.Application.Apps.Customers
{
    public class CustomerService : AppServiceBase, ICustomerService
    {
        public CustomerService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }


        //Get all Customer
        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Customer", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAll<Customer>(u => u.IsDeleted == false).ToListAsync();
                var data = Mappers.MapperList<Customer, CustomerDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //Get by customer id
        public async Task<ResultBase> GetById(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Customer", "GetById", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAsync<Customer>(u => u.IsDeleted == false && u.Id == input.Id);
                var data = Mappers.Mapper<Customer, CustomerDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }


        //Get customer by phone number
        public async Task<ResultBase> GetByPhone(RQCustomerByPhone input)
        {
            try
            {
                if (CheckExistSecCode("Customer", "GetByPhone", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAsync<Customer>(u => u.IsDeleted == false && u.PhoneNumber == input.PhoneNumber);
                var data = Mappers.Mapper<Customer, CustomerDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //Add customer
        public async Task<ResultBase> Add(RQCustomer input)
        {
            try
            {
                if (CheckExistSecCode("Customer", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var isExist = await repo.AnyAsync<Customer>(u => u.PhoneNumber == input.PhoneNumber);
                if (isExist)
                {
                    return ResultBase.Fail("Khách hàng đã tồn tại");
                }
                var data = Mappers.Mapper<RQCustomer, Customer>(input);
                data.CreatedBy = GetUserId();
                await repo.AddAsync<Customer>(data);
                return ResultBase.Success(input);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //update customer
        public async Task<ResultBase> Update(RQCustomer input)
        {
            try
            {
                if (CheckExistSecCode("Customer", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Customer>(input.Id);
                data.FullName = input.FullName;
                data.Gender = input.Gender;
                data.Birth = input.Birth;
                data.Address = input.Address;
                data.Email = input.Email;
                data.PhoneNumber = input.PhoneNumber;
                data.LastModifiedDate = DateTime.Now;
                data.LastModifiedBy = GetUserId();
                await repo.UpdateAsync<Customer>(data);
                return ResultBase.Success(input);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //delete customer
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Customer", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = repo.Get<Customer>(input.Id);
                data.IsDeleted = true;
                data.DeletedDate = DateTime.Now;
                data.DeletedBy = GetUserId();
                await repo.UpdateAsync<Customer>(data);
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

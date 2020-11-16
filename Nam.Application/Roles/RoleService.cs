using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Roles.Dto;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using Nam.Ultilities.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Roles
{
    public class RoleService : AppServiceBase, IRoleService
    {
        public RoleService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }

        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Role", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAll<Role>(u => u.IsDeleted == false).ToListAsync();
                var data = Mappers.MapperList<Role, RoleDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //add new Role
        public async Task<ResultBase> Add(RQRoleAdd input)
        {
            try
            {
                if (CheckExistSecCode("Role", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var isExist = await repo.AnyAsync<Role>(u => u.DisplayName == input.DisplayName);
                if (isExist)
                {
                    return ResultBase.Fail("Role is already exist");
                }
                var data = Mappers.Mapper<RQRoleAdd, Role>(input);
                data.CreatedBy = GetUserId();
                await repo.AddAsync<Role>(data);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //update Role
        public async Task<ResultBase> Update(RQRoleAdd input)
        {
            try
            {
                if (CheckExistSecCode("Role", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var isExist = await repo.AnyAsync<Role>(u => u.DisplayName == input.DisplayName);
                if (isExist)
                {
                    return ResultBase.Fail("Role is already exist");
                }
                var data = await repo.GetAsync<Role>(input.Id);
                data.DisplayName = input.DisplayName;
                data.LastModifiedBy = GetUserId();
                data.LastModifiedDate = DateTime.Now;
                await repo.UpdateAsync<Role>(data);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //delete Role
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Role", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Role>(input.Id);
                data.IsDeleted = true;
                data.DeletedBy = GetUserId();
                data.DeletedDate = DateTime.Now;
                await repo.UpdateAsync<Role>(data);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

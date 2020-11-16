using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Shops.Dto;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using Nam.Core.ResultBase.Messages;
using Nam.Ultilities.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Shops
{
    public class ShopService : AppServiceBase, IShopService
    {
        public ShopService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }

        //get all shop
        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Shop", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAll<Shop>(u => u.IsDeleted == false).ToListAsync();
                var data = Mappers.MapperList<Shop, ShopDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //get by shop id
        public async Task<ResultBase> GetById(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Shop", "GetById", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAsync<Shop>(u => u.IsDeleted == false && u.Id == input.Id);
                var data = Mappers.Mapper<Shop, ShopDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //add new shop
        public async Task<ResultBase> Add(RQShopAdd input)
        {
            try
            {
                if (CheckExistSecCode("Shop", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = Mappers.Mapper<RQShopAdd, Shop>(input);
                data.CreatedBy = GetUserId();
                await repo.AddAsync<Shop>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //update shop
        public async Task<ResultBase> Update(RQShopAdd input)
        {
            try
            {
                if (CheckExistSecCode("Shop", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = Mappers.Mapper<RQShopAdd, Shop>(input);
                data.LastModifiedBy = GetUserId();
                data.LastModifiedDate = DateTime.Now;
                await repo.UpdateAsync<Shop>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //update shop
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Shop", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Shop>(input.Id);
                data.DeletedBy = GetUserId();
                data.DeletedDate = DateTime.Now;
                data.IsDeleted = true;
                await repo.UpdateAsync<Shop>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

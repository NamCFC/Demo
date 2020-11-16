using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Categories.Dto;
using Nam.Application.Extensions;
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

namespace Nam.Application.Apps.Categories
{
    public class CategoryService : AppServiceBase, ICategoryService
    {
        public CategoryService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }


        //get all category
        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Category", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = await repo.GetAll<Category>(u => u.IsDeleted == false).ToListAsync();
                var data = Mappers.MapperList<Category, CategoryDto>(obj);
                return ResultBase.Success(data);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //add new category
        public async Task<ResultBase> Add(RQCategoryAdd input)
        {
            try
            {
                if (CheckExistSecCode("Category", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = Mappers.Mapper<RQCategoryAdd, Category>(input);
                data.CreatedBy = GetUserId();
                await repo.AddAsync<Category>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //update category
        public async Task<ResultBase> Update(RQCategoryAdd input)
        {
            try
            {
                if (CheckExistSecCode("Category", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Category>(input.Id);
                data.Name = input.Name;
                data.LastModifiedBy = GetUserId();
                data.LastModifiedDate = DateTime.Now;
                await repo.UpdateAsync<Category>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //delete Category by id
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Category", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = await repo.GetAsync<Category>(u => u.Id == input.Id);
                data.DeletedBy = GetUserId();
                data.DeletedDate = DateTime.Now;
                data.IsDeleted = true;
                await repo.UpdateAsync<Category>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

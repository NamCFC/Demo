using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Products.Dto;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using Nam.Core.ResultBase.Messages;
using Nam.Ultilities.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Products
{
    public class ProductService : AppServiceBase, IProductService
    {
        public ProductService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }

        //get all product
        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Product", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = (from product in repo.GetAll<Product>(u => u.IsDeleted == false).ToList()
                            join storage in repo.GetAll<Storage>() on product.Id equals storage.ProductId into st
                            from prd in st.DefaultIfEmpty()
                            select new ProductDto
                            {
                                Name = product.Name,
                                ImageUrl = product.ImageUrl,
                                Price = product.Price,
                                Storage = prd?.Quantity ?? 0
                            }).ToList();
                return await Task.FromResult(ResultBase.Success(data));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }

        //get by product id
        public async Task<ResultBase> GetById(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Product", "GetById", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = (from product in repo.GetAll<Product>(u => u.IsDeleted == false).ToList()
                            join storage in repo.GetAll<Storage>() on product.Id equals storage.ProductId into st
                            from prd in st.DefaultIfEmpty()
                            where product.Id == input.Id
                            select new ProductDto
                            {
                                Name = product.Name,
                                ImageUrl = product.ImageUrl,
                                Price = product.Price,
                                Storage = prd?.Quantity ?? 0
                            }).SingleOrDefault();
                return await Task.FromResult(ResultBase.Success(data));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }

        //get list product by category id
        public async Task<ResultBase> GetListByCategoryId(RQProductByCategoryId input)
        {
            try
            {
                if (CheckExistSecCode("Product", "GetListByCategoryId", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var data = (from product in repo.GetAll<Product>(u => u.IsDeleted == false).ToList()
                            join storage in repo.GetAll<Storage>() on product.Id equals storage.ProductId into st
                            from prd in st.DefaultIfEmpty()
                            where product.CategoryId == input.CategoryId
                            select new ProductDto
                            {
                                Name = product.Name,
                                ImageUrl = product.ImageUrl,
                                Price = product.Price,
                                Storage = prd?.Quantity ?? 0
                            }).ToList();
                return await Task.FromResult(ResultBase.Success(data));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }

        //add new Product
        public async Task<ResultBase> Add(RQProductAdd input)
        {
            try
            {
                if (CheckExistSecCode("Product", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var isExist = await repo.AnyAsync<Product>(u => u.Name == input.Name && u.CategoryId == input.CategoryId);
                if (isExist)
                {
                    return ResultBase.Fail("Product is already exist");
                }
                var product = Mappers.Mapper<RQProductAdd, Product>(input);
                product.CreatedBy = GetUserId();
                var addProduct = await repo.AddAsync<Product>(product);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //update Product
        public async Task<ResultBase> Update(RQProductAdd input)
        {
            try
            {
                if (CheckExistSecCode("Product", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var isExist = await repo.AnyAsync<Product>(u => u.Name == input.Name && u.CategoryId == input.CategoryId && u.Id != input.Id);
                if (isExist)
                {
                    return ResultBase.Fail("Product is already exist");
                }
                var data = await repo.GetAsync<Product>(input.Id);
                data.CategoryId = input.CategoryId;
                data.ImageUrl = input.ImageUrl;
                data.Name = input.Name;
                data.Price = input.Price;
                data.LastModifiedBy = GetUserId();
                data.LastModifiedDate = DateTime.Now;
                await repo.UpdateAsync<Product>(data);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //delete Product
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Product", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var product = await repo.GetAll<Product>(u => u.Id == input.Id).SingleOrDefaultAsync();
                product.DeletedBy = GetUserId();
                product.DeletedDate = DateTime.Now;
                product.IsDeleted = true;
                await repo.UpdateAsync<Product>(product);
                return ResultBase.Success(MessageBase.Success);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

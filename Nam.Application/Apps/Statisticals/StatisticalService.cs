using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Statisticals.Dto;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.ResultBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Statisticals
{
    public class StatisticalService : AppServiceBase, IStatisticalService
    {
        public StatisticalService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }

        //Statistic by date
        public async Task<ResultBase> StatisticByDay(RQStatisticByDay input)
        {
            try
            {
                if (CheckExistSecCode("Statistical", "StatisticByDay", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var statisticalShop = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                       join shop in repo.GetAll<Shop>(u => u.IsDeleted == false) on bill.ShopId equals shop.Id
                                       join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                       where bill.CreatedDate.Date == input.Day
                                       group billDetail by new { shop.Address } into stas
                                       select new StatisticalShopDto
                                       {
                                           QuantityProductsSold = stas.Sum(u => u.Quantity),
                                           ShopAddress = stas.Key.Address,
                                           TotalRevenue = stas.Sum(u => u.Amount)
                                       }).ToList();
                var statisticalProduct = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                          join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                          join product in repo.GetAll<Product>() on billDetail.ProductId equals product.Id
                                          where bill.CreatedDate.Date == input.Day
                                          group billDetail by new { product.Name, product.Id } into stas
                                          select new StatisticalProductDto
                                          {
                                              ProductId = stas.Key.Id,
                                              ProductName = stas.Key.Name,
                                              Quantity = stas.Sum(u => u.Quantity),
                                              TotalRevenue = stas.Sum(u => u.Amount)
                                          }).OrderByDescending(u => u.Quantity).ToList();
                StatisticalDto statistical = new StatisticalDto
                {
                    TotalRevenue = statisticalShop.Sum(u => u.TotalRevenue),
                    StatisticalShops = statisticalShop,
                    StatisticalProducts = statisticalProduct
                };
                return await Task.FromResult(ResultBase.Success(statistical));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }

        }

        //Statistic by month
        public async Task<ResultBase> StatisticByMonth(RQStatisticByMonth input)
        {
            try
            {
                if (CheckExistSecCode("Statistical", "StatisticByMonth", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var statisticalShop = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                       join shop in repo.GetAll<Shop>(u => u.IsDeleted == false) on bill.ShopId equals shop.Id
                                       join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                       where bill.CreatedDate.Month == input.Month && bill.CreatedDate.Year == input.Year
                                       group billDetail by new { shop.Address } into stas
                                       select new StatisticalShopDto
                                       {
                                           QuantityProductsSold = stas.Sum(u => u.Quantity),
                                           ShopAddress = stas.Key.Address,
                                           TotalRevenue = stas.Sum(u => u.Amount)
                                       }).ToList();
                var statisticalProduct = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                          join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                          join product in repo.GetAll<Product>() on billDetail.ProductId equals product.Id
                                          where bill.CreatedDate.Month == input.Month && bill.CreatedDate.Year == input.Year
                                          group billDetail by new { product.Name, product.Id } into stas
                                          select new StatisticalProductDto
                                          {
                                              ProductId = stas.Key.Id,
                                              ProductName = stas.Key.Name,
                                              Quantity = stas.Sum(u => u.Quantity),
                                              TotalRevenue = stas.Sum(u => u.Amount)
                                          }).OrderByDescending(u => u.Quantity).ToList();
                StatisticalDto statistical = new StatisticalDto
                {
                    TotalRevenue = statisticalShop.Sum(u => u.TotalRevenue),
                    StatisticalShops = statisticalShop,
                    StatisticalProducts = statisticalProduct
                };
                return await Task.FromResult(ResultBase.Success(statistical));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }

        }

        //Statistic by year
        public async Task<ResultBase> StatisticByYear(RQStatisticByYear input)
        {
            try
            {
                if (CheckExistSecCode("Statistical", "StatisticByYear", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var statisticalShop = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                       join shop in repo.GetAll<Shop>(u => u.IsDeleted == false) on bill.ShopId equals shop.Id
                                       join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                       where bill.CreatedDate.Year == input.Year
                                       group billDetail by new { shop.Address } into stas
                                       select new StatisticalShopDto
                                       {
                                           QuantityProductsSold = stas.Sum(u => u.Quantity),
                                           ShopAddress = stas.Key.Address,
                                           TotalRevenue = stas.Sum(u => u.Amount)
                                       }).ToList();
                var statisticalProduct = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                         join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                         join product in repo.GetAll<Product>() on billDetail.ProductId equals product.Id
                                         where bill.CreatedDate.Year == input.Year
                                         group billDetail by new { product.Name, product.Id } into stas
                                         select new StatisticalProductDto
                                         {
                                             ProductId = stas.Key.Id,
                                             ProductName = stas.Key.Name,
                                             Quantity = stas.Sum(u => u.Quantity),
                                             TotalRevenue = stas.Sum(u => u.Amount)
                                         }).OrderByDescending(u => u.Quantity).ToList();
                StatisticalDto statistical = new StatisticalDto
                {
                    TotalRevenue = statisticalShop.Sum(u => u.TotalRevenue),
                    StatisticalShops = statisticalShop,
                    StatisticalProducts = statisticalProduct
                };
                return await Task.FromResult(ResultBase.Success(statistical));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }

        }

        public async Task<ResultBase> StatisticByFromTo(RQStatisticByFromTo input)
        {
            try
            {
                if (CheckExistSecCode("Statistical", "StatisticByFromTo", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var statisticalShop = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                       join shop in repo.GetAll<Shop>(u => u.IsDeleted == false) on bill.ShopId equals shop.Id
                                       join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                       where bill.CreatedDate >= input.TimeStart && bill.CreatedDate <= input.TimeEnd
                                       group billDetail by new { shop.Address } into stas
                                       select new StatisticalShopDto
                                       {
                                           QuantityProductsSold = stas.Sum(u => u.Quantity),
                                           ShopAddress = stas.Key.Address,
                                           TotalRevenue = stas.Sum(u => u.Amount)
                                       }).ToList();
                var statisticalProduct = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Status == true).ToList()
                                          join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                                          join product in repo.GetAll<Product>() on billDetail.ProductId equals product.Id
                                          where bill.CreatedDate >= input.TimeStart && bill.CreatedDate <= input.TimeEnd
                                          group billDetail by new { product.Name, product.Id } into stas
                                          select new StatisticalProductDto
                                          {
                                              ProductId = stas.Key.Id,
                                              ProductName = stas.Key.Name,
                                              Quantity = stas.Sum(u => u.Quantity),
                                              TotalRevenue = stas.Sum(u => u.Amount)
                                          }).OrderByDescending(u => u.Quantity).ToList();
                StatisticalDto statistical = new StatisticalDto
                {
                    TotalRevenue = statisticalShop.Sum(u => u.TotalRevenue),
                    StatisticalShops = statisticalShop,
                    StatisticalProducts = statisticalProduct
                };
                return await Task.FromResult(ResultBase.Success(statistical));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }
    }
}

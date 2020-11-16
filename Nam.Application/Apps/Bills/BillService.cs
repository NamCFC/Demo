using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Bills.Dto;
using Nam.Application.Apps.Employees.Dto;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.Request;
using Nam.Core.ResultBase;
using Nam.Ultilities.AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nam.Application.Apps.Bills
{
    public class BillService : AppServiceBase, IBillService
    {
        public BillService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }

        //get all bill
        public async Task<ResultBase> GetAll(RQParam input)
        {
            try
            {
                if (CheckExistSecCode("Bill", "GetAll", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false).ToList()
                           join customer in repo.GetAll<Customer>(u => u.IsDeleted == false) on bill.CustomerId equals customer.Id
                           join employee in repo.GetAll<Employee>(u => u.IsDeleted == false) on bill.EmployeeId equals employee.Id
                           join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId into b
                           select new BillDto
                           {
                               CustomerId = bill.CustomerId,
                               CustomerName = customer.FullName,
                               CustomerPhoneNumber = customer.PhoneNumber,
                               EmployeeName = employee.FullName,
                               AddressDelivery = bill.AddressDelivery,
                               EmployeeId = bill.EmployeeId,
                               Status = bill.Status,
                               PayType = bill.PayType,
                               ShopId = bill.ShopId,
                               CreatedDate = bill.CreatedDate,
                               TotalMoney = b.Sum(u => u.Amount)
                           }).ToList();
                return await Task.FromResult(ResultBase.Success(obj));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }


        //get all bill by customer id
        public async Task<ResultBase> GetListBillByCustomerId(RQBillByCustomerId input)
        {
            try
            {
                if (CheckExistSecCode("Bill", "GetListBillByCustomerId", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.CustomerId == input.CustomerId).ToList()
                           join customer in repo.GetAll<Customer>(u => u.IsDeleted == false) on bill.CustomerId equals customer.Id
                           join employee in repo.GetAll<Employee>(u => u.IsDeleted == false) on bill.EmployeeId equals employee.Id
                           join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId into bd
                           select new BillDto
                           {
                               CustomerId = bill.CustomerId,
                               CustomerName = customer.FullName,
                               AddressDelivery = bill.AddressDelivery,
                               EmployeeId = bill.EmployeeId,
                               EmployeeName = employee.FullName,
                               Status = bill.Status,
                               PayType = bill.PayType,
                               TotalMoney = bd.Sum(u => u.Amount)
                           }).ToList();
                return await Task.FromResult(ResultBase.Success(obj));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }

        //get bill detail by bill id
        public async Task<ResultBase> GetListBillDetailByBillId(RQBillDetailByBillId input)
        {
            try
            {
                if (CheckExistSecCode("Bill", "GetListBillDetailByBillId", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var obj = (from bill in repo.GetAll<Bill>(u => u.IsDeleted == false && u.Id == input.BillId).ToList()
                           join customer in repo.GetAll<Customer>() on bill.CustomerId equals customer.Id
                           join employee in repo.GetAll<Employee>() on bill.EmployeeId equals employee.Id
                           join billDetail in repo.GetAll<BillDetail>() on bill.Id equals billDetail.BillId
                           group billDetail by new { bill, customer, employee } into bd
                           select new BillDto
                           {
                               CustomerId = bd.Key.bill.CustomerId,
                               EmployeeId = bd.Key.bill.EmployeeId,
                               CustomerName = bd.Key.customer.FullName,
                               EmployeeName = bd.Key.employee.FullName,
                               CustomerPhoneNumber = bd.Key.customer.PhoneNumber,
                               ShopId = bd.Key.bill.ShopId,
                               CreatedDate = bd.Key.bill.CreatedDate,
                               PayType = bd.Key.bill.PayType,
                               Status = bd.Key.bill.Status,
                               TotalMoney = bd.Sum(u => u.Amount),
                               AddressDelivery = bd.Key.bill.AddressDelivery,
                               BillDetails = bd.Join(repo.GetAll<Product>(u => u.IsDeleted == false), p => p.ProductId, q => q.Id, (p, q) => new {p, q})
                               .Select(u => new BillDetailDto
                               {
                                   ProductId = u.p.ProductId,
                                   Quantity = u.p.Quantity,
                                   Amount = u.p.Amount,
                                   ProductImage = u.q.ImageUrl,
                                   ProductName = u.q.Name
                               }).ToList()
                           }).SingleOrDefault();
                return await Task.FromResult(ResultBase.Success(obj));
            }
            catch
            {
                return await Task.FromResult(ResultBase.FailSystem());
            }
        }


        // add bill
        public async Task<ResultBase> Add(RQBillAdd input)
        {
            try
            {
                if (CheckExistSecCode("Bill", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }

                //check tồn kho
                foreach(var bd in input.RQBillDetails)
                {
                    var storage = await repo.GetAsync<Storage>(u => u.ProductId == bd.ProductId && u.ShopId == input.ShopId);
                    if (storage == null || storage.Quantity < bd.Quantity)
                    {
                        return ResultBase.Fail(string.Format("Số lượng sản phẩm '{0}' không đủ", bd.ProductId));
                    }
                }

                var bill = Mappers.Mapper<RQBillAdd, Bill>(input);
                bill.EmployeeId = GetCurrentEmployee().Id;
                var addBill = await repo.AddAsync<Bill>(bill);
                var billDetail = Mappers.MapperList<RQBillDetail, BillDetail>(input.RQBillDetails);
                foreach(var bd in billDetail)
                {
                    bd.BillId = addBill.Id;
                    var product = repo.Get<Product>(bd.ProductId);
                    bd.Amount = (decimal)(bd.Quantity * product.Price);
                    await repo.AddAsync<BillDetail>(bd);
                    var storage = repo.Get<Storage>(u => u.ProductId == bd.ProductId && u.ShopId == bill.ShopId);
                    storage.Quantity -= bd.Quantity;
                    await repo.UpdateAsync<Storage>(storage);
                }
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }


        //update bill
        public async Task<ResultBase> Update(RQBillAdd input)
        {
            try
            {
                if (CheckExistSecCode("Bill", "Update", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                //check tồn kho
                foreach (var bd in input.RQBillDetails)
                {
                    var billD = await repo.GetAsync<BillDetail>(u => u.ProductId == bd.ProductId);
                    if(billD != null)
                    {
                        if(billD.Quantity < bd.Quantity)
                        {
                            var storage = await repo.GetAsync<Storage>(u => u.ProductId == bd.ProductId && u.ShopId == input.ShopId);
                            if (storage == null || storage.Quantity < bd.Quantity)
                            {
                                return ResultBase.Fail(string.Format("Số lượng sản phẩm '#{0}' không đủ", bd.ProductId));
                            }
                            billD.Quantity = bd.Quantity;
                            await repo.UpdateAsync<BillDetail>(billD);
                        }
                        if (billD.Quantity > bd.Quantity)
                        {
                            billD.Quantity = bd.Quantity;
                            await repo.UpdateAsync<BillDetail>(billD);
                        }
                    }
                    else
                    {
                        var billDetail = Mappers.Mapper<RQBillDetail, BillDetail>(bd);
                        billDetail.BillId = input.Id;
                        await repo.AddAsync<BillDetail>(billDetail);
                    }
                }
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //delete bill
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("Bill", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var bill = repo.Get<Bill>(u => u.IsDeleted == false && u.Id == input.Id);
                if(bill == null)
                {
                    return ResultBase.Fail(string.Format("Đơn hàng #{0} không tồn tại", input.Id));
                }
                var billDetail = await repo.GetAll<BillDetail>(u => u.BillId == input.Id).ToListAsync();
                foreach(var bd in billDetail)
                {
                    var storage = repo.Get<Storage>(u => u.ProductId == bd.ProductId && u.ShopId == bill.ShopId);
                    storage.Quantity += bd.Quantity;
                }
                bill.IsDeleted = true;
                bill.Status = false;
                await repo.UpdateAsync<Bill>(bill);
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

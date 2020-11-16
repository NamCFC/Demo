using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nam.Application.Apps.Bills;
using Nam.Application.Apps.Categories;
using Nam.Application.Apps.Customers;
using Nam.Application.Apps.Employees;
using Nam.Application.Apps.Products;
using Nam.Application.Apps.Shops;
using Nam.Application.Apps.Statisticals;
using Nam.Application.Roles;
using Nam.Application.Users;
using Nam.Core.Repositories;
using Nam.EFCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Application.Common
{
    public static class DependencyInjectionConfigurations
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository<EFDbContext>>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStatisticalService, StatisticalService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

    }
}

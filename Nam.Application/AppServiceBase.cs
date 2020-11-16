using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Nam.Application.Apps.Employees.Dto;
using Nam.Application.Users;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Ultilities.AutoMapper;
using Nam.Ultilities.ReadFile;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nam.Core.SecCode.SecCode;

namespace Nam.Application
{
    public abstract class AppServiceBase
    {
        protected readonly IRepository repo;
        protected readonly IConfiguration config;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected AppServiceBase(
            IRepository _repo,
            IConfiguration _config,
            IHttpContextAccessor _httpContextAccessor)
        {
            repo = _repo;
            config = _config;
            httpContextAccessor = _httpContextAccessor;
        }


        protected bool CheckExistSecCode(string Ctrl, string Func, string SecCode)
        {
            bool result = false;
            try
            {
                string path = config["Config:SecCode"];
                var listSecCode = ReadFileXML<ListSecCode>.getServerNodeXMLRangeValueDB(path, "SECCODE");
                string secCode = listSecCode.Find(u => u.SecCode.Controller == Ctrl && u.SecCode.Function == Func).SecCode.Code;
                result = true;
                if (secCode != SecCode || SecCode == "")
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        protected long GetUserId()
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.Single(u => u.Type == "Id").Value;
            return Convert.ToInt64(userId);
        }

        protected EmployeeDto GetCurrentEmployee()
        {
            try
            {
                var employee = repo.Get<Employee>(u => u.UserId == Convert.ToInt64(GetUserId()));
                var result = Mappers.Mapper<Employee, EmployeeDto>(employee);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}

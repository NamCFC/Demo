using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nam.Application.Users.Dto;
using Nam.Application.Authentication.JwtBearer;
using Nam.Core.Entities;
using Nam.Core.Repositories;
using Nam.Core.ResultBase;
using Nam.Ultilities.AutoMapper;
using Nam.Ultilities.Encrypts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography;
using System.Linq;
using Nam.Core.Request;
using Microsoft.AspNetCore.Http;

namespace Nam.Application.Users
{
    public class UserService : AppServiceBase, IUserService
    {
        public UserService(IRepository repo, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(repo, config, httpContextAccessor)
        {

        }

        public async Task<ResultBase> Login(RQUserLogin input)
        {
            try
            {
                if (CheckExistSecCode("User", "Login", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                if (input.UserName != "" && input.Password != "")
                {
                    string hashPass = EncryptMD5.CreateMD5(input.Password);
                    var user = await (from u in repo.GetAll<User>(u => u.UserName == input.UserName && u.Password == hashPass && u.IsActive == true && u.IsDeleted == false)
                                      join ur in repo.GetAll<UserRole>() on u.Id equals ur.UserId
                                      join r in repo.GetAll<Role>() on ur.RoleId equals r.Id
                                      select new UserInfoDto
                                      {
                                          Id = u.Id,
                                          UserName = u.UserName,
                                          Role = r.DisplayName
                                      }).SingleOrDefaultAsync();
                    if (user != null)
                    {
                        user.JwtToken = JwtToken.GenerateJSONWebToken(user, config);
                        return ResultBase.Success(user);
                    }
                    else
                    {
                        return ResultBase.Fail();
                    }
                }
                else
                {
                    return ResultBase.Fail();
                }
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //Add new account
        public async Task<ResultBase> Add(RQUserRegister input)
        {
            try
            {
                if (CheckExistSecCode("User", "Add", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var isExist = await repo.AnyAsync<User>(u => u.UserName == input.UserName);
                if (isExist)
                {
                    return ResultBase.Fail("User is already exist");
                }
                if(input.Password != input.ConfirmPassword)
                {
                    return ResultBase.Fail("Confirm Password wrong");
                }
                string hashPass = EncryptMD5.CreateMD5(input.Password);
                User user = new User
                {
                    UserName = input.UserName,
                    Password = hashPass
                };
                var addUser = await repo.AddAsync<User>(user);
                UserRole userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = input.RoleId
                };
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //Update account
        public async Task<ResultBase> ChangePassword(RQUserChangePassword input)
        {
            try
            {
                if (CheckExistSecCode("User", "LockAccount", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                if(input.NewPassword != input.ConfirmNewPassword)
                {
                    return ResultBase.Fail("Confirm Password wrong");
                }
                string hashCurrentPass = EncryptMD5.CreateMD5(input.CurrentPassword);
                var user = await repo.GetAsync<User>(u => u.Id == input.UserId && u.Password == hashCurrentPass);
                if(user != null)
                {
                    string hashPass = EncryptMD5.CreateMD5(input.NewPassword);
                    user.Password = hashPass;
                    user.LastModifiedDate = DateTime.Now;
                }
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //Lock account
        public async Task<ResultBase> LockAccount(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("User", "LockAccount", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var user = await repo.GetAsync<User>(u => u.Id == input.Id);
                user.IsActive = false;
                user.LastModifiedDate = DateTime.Now;
                await repo.UpdateAsync<User>(user);
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }

        //Delete account
        public async Task<ResultBase> Delete(RQParamId input)
        {
            try
            {
                if (CheckExistSecCode("User", "Delete", input.SecCode) == false)
                {
                    return ResultBase.SecCodeWrong();
                }
                var user = await repo.GetAsync<User>(u => u.Id == input.Id);
                user.IsDeleted = true;
                user.DeletedDate = DateTime.Now;
                await repo.UpdateAsync<User>(user);
                return ResultBase.Success(null);
            }
            catch
            {
                return ResultBase.FailSystem();
            }
        }
    }
}

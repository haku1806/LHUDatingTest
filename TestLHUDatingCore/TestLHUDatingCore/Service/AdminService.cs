using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestLHUDatingCore.Dto;
using TestLHUDatingCore.Model;
using TestLHUDatingCore.Ulti;

namespace TestLHUDatingCore.Service
{
    public class AdminService : IServiceBase<AdminDto, string>
    {
        protected readonly MyContext context;

        public AdminService(MyContext context)
        {
            this.context = context;
        }

        public void DeleteById(string key, string userSession = null)
        {
            Admin admin = this.context.Admins.FirstOrDefault(x => x.UserName == key);
            this.context.Admins.Remove(admin);

            this.context.SaveChanges();
        }

        public List<AdminDto> GetAll()
        {
            return this.context.Admins
                .Select(x => new AdminDto()
                {
                    //Code = x.Code,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    //Role = x.Role,
                    LastLogin = x.LastLogin,
                    Active = x.Active
                })
                .ToList();
        }

        public List<AdminDto> Get(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
                key = null;

            return this.context.Admins
                .Where(x => key == null || x.UserName.Contains(key) || x.FullName.Contains(key))
                .Select(x => new AdminDto()
                {
                    //Code = x.Code,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    //Role = x.Role,
                    LastLogin = x.LastLogin,
                    Active = x.Active
                })
                .ToList();
        }
        public AdminDto GetById(string key)
        {
            return this.context.Admins
                .Where(x => x.UserName == key)
                .Select(x => new AdminDto()
                {
                    //Code = x.Code,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    //Role = x.Role,
                    LastLogin = x.LastLogin,
                    Active = x.Active
                })
                .FirstOrDefault();
        }

        public AdminDto Insert(AdminDto entity)
        {
            if (this.context.Admins.Any(x => x.UserName == entity.UserName))
                throw new AggregateException("Tên tài khoản đã tồn tại.");

            if (this.context.Admins.Any(x => x.Email == entity.Email))
                throw new AggregateException("Email đã tồn tại.");

            if (this.context.Admins.Any(x => x.Phone == entity.Phone))
                throw new AggregateException("Số điện thoại đã tồn tại.");

            Admin admin = new Admin()
            {
                //Code = Guid.NewGuid().ToString("N"),
                UserName = entity.UserName,
                FullName = entity.FullName,
                Email = entity.Email,
                Phone = entity.Phone,
                //Role = entity.Role,
                Password = DataHelper.SHA256Hash(Constrants.Encrypt.DefautEncrypt + entity.Email + "_" + entity.Password)
            };

            this.context.Admins.Add(admin);
            this.context.SaveChanges();

            return entity;
        }

        public void Update(string key, AdminDto entity)
        {
            Admin admin = this.context.Admins.FirstOrDefault(x => x.UserName == key);

            admin.Phone = entity.Phone;
            admin.Active = entity.Active;
            admin.Email = entity.Email;
            admin.FullName = entity.FullName;
            //admin.Role = entity.Role;

            this.context.SaveChanges();
        }

        public void ResetPassword(string key, string newPassword)
        {
            Admin admin = this.context.Admins.FirstOrDefault(x => x.UserName == key);

            admin.Password = DataHelper.SHA256Hash(Constrants.Encrypt.DefautEncrypt + admin.UserName + "_" + newPassword);
            this.context.SaveChanges();
        }
        
        public object GetAccessToken(AdminDto entity)
        {
            Admin admin = this.context.Admins
                .Where(x => x.Active)
                .FirstOrDefault(x => x.UserName == entity.UserName);

            if (admin == null)
                throw new AggregateException("Tài khoản hoặc mật khẩu không đúng.");

            string passwordCheck = DataHelper.SHA256Hash(Constrants.Encrypt.DefautEncrypt + entity.UserName + "_" + entity.Password);

            if (admin.Password != passwordCheck)
                throw new AggregateException("Tài khoản hoặc mật khẩu không đúng.");

            admin.LastLogin = DateTime.Now;
            this.context.SaveChanges();

            DateTime expirationDate = DateTime.Now.Date.AddMinutes(Constrants.JwtConfig.ExpirationInMinutes);
            long expiresAt = (long)(expirationDate - new DateTime(1970, 1, 1)).TotalSeconds;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constrants.JwtConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.UserData, admin.UserName),
                    new Claim(ClaimTypes.Expiration, expiresAt.ToString())
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new
            {
                admin.UserName,
                admin.FullName,
                Token = tokenHandler.WriteToken(token),
                ExpiresAt = expiresAt
            };  

        }
    }
}

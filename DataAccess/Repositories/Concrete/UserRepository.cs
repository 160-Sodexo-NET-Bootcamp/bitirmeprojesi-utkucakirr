using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Entities.DataModel;
using Entities.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Registering a user.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> Register(RegisterModel input)
        {
            var list = await base.GetAll();
            foreach(var item in list)
            {
                if(item.Email == input.Email)
                {
                    return false;
                }
            }
            string salt = GetSalt();
            var user = new User
            {
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Salt = salt,
                PasswordHash = GetPasswordHash(input.Password,salt)

            };

            await base.AddAsync(user);
            return true;
        }

        /// <summary>
        /// Login process for a user.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<User> Login(LoginModel input)
        {
            var temp = await base.GetByExpression(x => x.Email == input.Email);
            if(temp is null)
            {
                return null;
            }
            if (temp.FailCount == 3)
            {
                return temp;
            }
            if (!Verify(input.Password,temp.PasswordHash,temp.Salt))
            {
                temp.FailCount++;
                base.Update(temp);
                return null;
            }
            temp.FailCount = 0;
            base.Update(temp);
            return temp;
        }

        /// <summary>
        /// Password reset process for a user.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> Reset(PWResetModel input)
        {
            var temp = await base.GetByExpression(x => x.Email == input.Email);
            if(temp is null)
            {
                return false;
            }
            if(Verify(input.OldPassword,temp.PasswordHash,temp.Salt))
            {
                temp.PasswordHash = GetPasswordHash(input.NewPassword, temp.Salt);
                base.Update(temp);
                return true;
            }
            return false;
            
        }

        /// <summary>
        /// Getting salt for hashed password.
        /// </summary>
        /// <returns></returns>
        public string GetSalt()
        {
            byte[] saltBytes = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Hashing password.
        /// </summary>
        /// <param name="pw"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetPasswordHash(string pw, string salt)
        {
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        /// <summary>
        /// Verifying password for login process.
        /// </summary>
        /// <param name="pw"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public bool Verify(string pw, string hash, string salt)
        {
            return GetPasswordHash(pw, salt) == hash;
        }
    }
}

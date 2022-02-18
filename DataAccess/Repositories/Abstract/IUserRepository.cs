using Entities.DataModel;
using Entities.Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<bool> Register(RegisterModel input);
        Task<User> Login(LoginModel input);
        Task<bool> Reset(PWResetModel input);

        string GetSalt();
        string GetPasswordHash(string pw, string salt);
        bool Verify(string pw, string hash, string salt);

    }
}

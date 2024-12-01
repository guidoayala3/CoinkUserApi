using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Repositories
{
    public interface IUserRepository
    {
        Task<int> InsertUserAsync(UserDto user);
        Task<IEnumerable<UserView>> GetAllUsersAsync();
    }
}
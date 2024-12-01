using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Services
{
    public interface IUserService
    {
        Task<int> AddUserAsync(UserDto user);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}

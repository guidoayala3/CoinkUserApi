using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApi.Models;
using UserRegistrationApi.Repositories;

namespace UserRegistrationApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddUserAsync(UserDto user)
        {
            return await _repository.InsertUserAsync(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address,
                Municipality = user.Municipality,
                Department = user.Department,
                Country = user.Country
            });
        }
    }
}

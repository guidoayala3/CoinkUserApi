using Microsoft.AspNetCore.Mvc;
using UserRegistrationApi.Models;
using UserRegistrationApi.Services;
using FluentValidation;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserRegistrationApi.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserDto> _userValidator;

        public UsersController(IUserService userService, IValidator<UserDto> userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        // Endpoint para registrar un solo usuario (POST /api/user)
        [HttpPost("api/user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            // Validar el usuario utilizando FluentValidation
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // Retorna un error si no es válido
            }

            // Llamar al servicio para agregar el usuario
            var result = await _userService.AddUserAsync(user);
            return Ok(new { Message = "User added successfully", UserId = result });
        }

        // Endpoint para listar todos los usuarios (GET /api/users)
        [HttpGet("api/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}

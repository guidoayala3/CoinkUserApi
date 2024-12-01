using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistrationApi.Data;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> InsertUserAsync(UserDto user)
        {
            var sql = "SELECT sp_insert_user(@p_name, @p_phone, @p_address, @p_country_name, @p_department_name, @p_municipality_name)";
            var parameters = new List<NpgsqlParameter>
    {
        new NpgsqlParameter("p_name", user.Name.ToUpper()),
        new NpgsqlParameter("p_phone", user.Phone),
        new NpgsqlParameter("p_address", user.Address.ToUpper()),
        new NpgsqlParameter("p_country_name", user.Country.ToUpper()),
        new NpgsqlParameter("p_department_name", user.Department.ToUpper()),
        new NpgsqlParameter("p_municipality_name", user.Municipality.ToUpper())
    };

            using (var connection = (NpgsqlConnection)_context.Database.GetDbConnection())
            {
                try
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        var result = await command.ExecuteScalarAsync();
                        if (result == null || result == DBNull.Value)
                        {
                            throw new InvalidOperationException("No se pudo obtener el ID insertado.");
                        }

                        return (int)result;
                    }
                }
                catch (NpgsqlException ex) when (ex.SqlState == "23505")
                {
                    throw new InvalidOperationException("Ya existe un usuario con los mismos datos (nombre, teléfono y dirección).", ex);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Ocurrió un error al insertar el usuario.", ex);
                }
            }
        }


        // Método para obtener todos los usuarios
        public async Task<IEnumerable<UserView>> GetAllUsersAsync()
        {
            return await _context.UserViews.FromSqlRaw("SELECT * FROM user_view").ToListAsync();
        }
    }
}

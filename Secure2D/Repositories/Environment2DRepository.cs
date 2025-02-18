using System.Data;
using Dapper;
using Secure2D.Models;
using Microsoft.Data.SqlClient;

namespace Secure2D.Repositories
{
    public class Environment2DRepository : IEnvironment2DRepository
    {
        private readonly string _connectionString;

        public Environment2DRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("SqlConnectionString");
        }

        public async Task<IEnumerable<Environment2D>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Environment2D>("SELECT * FROM Environment2D");
        }

        public async Task<Environment2D?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Environment2D>(
                "SELECT * FROM Environment2D WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(Environment2D environment)
        {
            environment.Id = Guid.NewGuid();
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Environment2D (Id,Name, MaxHeight, MaxLength) VALUES (@Id, @Name, @MaxHeight, @MaxLength)";
            await connection.ExecuteAsync(sql, environment);
        }

        public async Task UpdateAsync(Environment2D environment)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Environment2D SET Name = @Name, MaxHeight = @MaxHeight, MaxLength = @MaxLength WHERE Id = @Id";
            await connection.ExecuteAsync(sql, environment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Environment2D WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}

using System.Data;
using Dapper;
using Secure2D.Models;
using Microsoft.Data.SqlClient;

namespace Secure2D.Repositories
{
    public class Object2DRepository : IObject2DRepository
    {
        private readonly string _connectionString;

        public Object2DRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("SqlConnectionString");
        }

        public async Task<IEnumerable<Object2D>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Object2D>("SELECT * FROM Object2D");
        }

        public async Task<Object2D?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Object2D>(
                "SELECT * FROM Object2D WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Object2D>> GetByEnvironmentIdAsync(Guid environmentId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Object2D>(
                "SELECT * FROM Object2D WHERE EnvironmentId = @EnvironmentId", new { EnvironmentId = environmentId });
        }

        public async Task AddAsync(Object2D obj)
        {
            obj.Id = Guid.NewGuid();
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
                INSERT INTO Object2D (Id, EnvironmentId, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer)
                VALUES (@Id, @EnvironmentId, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)";
            await connection.ExecuteAsync(sql, obj);
        }

        public async Task UpdateAsync(Object2D obj)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
                UPDATE Object2D 
                SET EnvironmentId = @EnvironmentId, PrefabId = @PrefabId, PositionX = @PositionX, PositionY = @PositionY, 
                    ScaleX = @ScaleX, ScaleY = @ScaleY, RotationZ = @RotationZ, SortingLayer = @SortingLayer 
                WHERE Id = @Id";
            await connection.ExecuteAsync(sql, obj);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Object2D WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}

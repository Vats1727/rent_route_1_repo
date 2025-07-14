using Microsoft.Data.SqlClient;
using System.Data;
using rentroute1.Models;

namespace rentroute1.Services
{
    public class CarService
    {
        private readonly string _connectionString;

        public CarService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddOrUpdateCar(AdminCars car)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("AddOrUpdateCar", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", car.Id == 0 ? (object)DBNull.Value : car.Id);
                command.Parameters.AddWithValue("@Title", car.Title);
                command.Parameters.AddWithValue("@ImageData", car.ImageData);
                command.Parameters.AddWithValue("@Details", car.Details);
                command.Parameters.AddWithValue("@ModelType", car.ModelType);
                command.Parameters.AddWithValue("@Price", car.Price);
                command.Parameters.AddWithValue("@IsActive", car.IsActive);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<AdminCars>> GetCars(bool onlyActive = false)
        {
            var cars = new List<AdminCars>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = onlyActive
                    ? "SELECT * FROM AdminCars WHERE IsActive = 1"
                    : "SELECT * FROM AdminCars";

                var command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cars.Add(new AdminCars
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            ImageData = reader["ImageData"].ToString(),
                            Details = reader["Details"].ToString(),
                            ModelType = reader["ModelType"].ToString(),
                            Price = (decimal)reader["Price"],
                            IsActive = (bool)reader["IsActive"]
                        });
                    }
                }
            }

            return cars;
        }
    }
}

using HospitalAppointmentBooking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace HospitalAppointmentBooking.Infrastructure.Data
{
    public class DbHelper : IDbHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HospitalAppointmentBooking");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

// Services/AppointmentQueryService.cs
using HospitalAppointmentBooking.Application.DTOs;
using HospitalAppointmentBooking.Application.Interfaces;
using Microsoft.Data.SqlClient;

namespace HospitalAppointmentBooking.Infrastructure.Services
{
    public class AppointmentQueryService : IAppointmentQueryService
    {
        private readonly IDbHelper _dbHelper;

        public AppointmentQueryService(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<AppointmentDetailDto> GetAllDetails()
        {
            var list = new List<AppointmentDetailDto>();

            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_GetAllAppointments", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new AppointmentDetailDto
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                    DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                    DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                    Specialization = reader.GetString(reader.GetOrdinal("Specialization")),
                    PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                    PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                });
            }

            return list;
        }

        public AppointmentDetailDto GetDetailById(int id)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_GetAppointmentById", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AppointmentId", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AppointmentDetailDto
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                    DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                    DoctorName = reader.GetString(reader.GetOrdinal("DoctorName")),
                    Specialization = reader.GetString(reader.GetOrdinal("Specialization")),
                    PatientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                    PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
            }

            throw new Exception("Appointment not found.");
        }
    }
}

// Repository/AppointmentRepository.cs
using HospitalAppointmentBooking.Application.Interfaces;
using HospitalAppointmentBooking.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HospitalAppointmentBooking.Infrastructure.Repository
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly IDbHelper _dbHelper;

        public AppointmentRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void Add(Appointment entity)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_AddAppointment", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DoctorId", entity.DoctorId);
            cmd.Parameters.AddWithValue("@PatientId", entity.PatientId);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@Remarks", (object?)entity.Remarks ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Update(Appointment entity)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_UpdateAppointment", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AppointmentId", entity.AppointmentId);
            cmd.Parameters.AddWithValue("@DoctorId", entity.DoctorId);
            cmd.Parameters.AddWithValue("@PatientId", entity.PatientId);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@Remarks", (object?)entity.Remarks ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_DeleteAppointment", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AppointmentId", id);
            cmd.ExecuteNonQuery();
        }

        public List<Appointment> GetAll()
        {
            var list = new List<Appointment>();

            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_GetAllAppointmentsRaw", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Appointment
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                    DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                    PatientId = reader.GetInt32(reader.GetOrdinal("PatientId"))
                });
            }

            return list;
        }

        public Appointment GetById(int id)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_GetAppointmentByIdRaw", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AppointmentId", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Appointment
                {
                    AppointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                    DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                    PatientId = reader.GetInt32(reader.GetOrdinal("PatientId"))
                };
            }

            throw new Exception("Appointment not found.");
        }
    }
}

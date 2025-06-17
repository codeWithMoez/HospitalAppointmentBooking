using HospitalAppointmentBooking.Application.Interfaces;
using HospitalAppointmentBooking.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HospitalAppointmentBooking.Infrastructure.Repository
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly IDbHelper _dbHelper;

        public PatientRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void Add(Patient entity)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_AddPatient", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_DeletePatient", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@PatientId", id);
            cmd.ExecuteNonQuery();
        }

        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_GetAllPatients", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                patients.Add(new Patient
                {
                    PatientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }
            return patients;
        }

        public Patient GetById(int id)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_GetPatientById", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@PatientId", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Patient
                {
                    PatientId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2)
                };
            }
            throw new Exception("Patient not found.");
        }

        public void Update(Patient entity)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SP_UpdatePatient", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@PatientId", entity.PatientId);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.ExecuteNonQuery();
        }
    }
}

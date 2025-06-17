using HospitalAppointmentBooking.Application.Interfaces;
using HospitalAppointmentBooking.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HospitalAppointmentBooking.Infrastructure.Repository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly IDbHelper _dbHelper;

        public DoctorRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void Add(Doctor entity)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SP_AddDoctor", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Specialization", entity.Specialization);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL error while adding doctor.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SP_DeleteDoctor", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DoctorId", id);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL error while deleting doctor.", ex);
            }
        }

        public List<Doctor> GetAll()
        {
            var doctors = new List<Doctor>();
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SP_GetAllDoctors", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    doctors.Add(new Doctor
                    {
                        DoctorId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Specialization = reader.GetString(2)
                    });
                }
                return doctors;
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL error while fetching doctors.", ex);
            }
        }

        public Doctor GetById(int id)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SP_GetDoctorById", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DoctorId", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Doctor
                    {
                        DoctorId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Specialization = reader.GetString(2)
                    };
                }
                throw new Exception("Doctor not found.");
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL error while fetching doctor by ID.", ex);
            }
        }

        public void Update(Doctor entity)
        {
            try
            {
                using var conn = _dbHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand("SP_UpdateDoctor", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DoctorId", entity.DoctorId);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Specialization", entity.Specialization);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL error while updating doctor.", ex);
            }
        }
    }
}

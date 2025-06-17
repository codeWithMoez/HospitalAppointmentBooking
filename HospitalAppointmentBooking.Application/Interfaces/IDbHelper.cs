using Microsoft.Data.SqlClient;

namespace HospitalAppointmentBooking.Application.Interfaces
{
    public interface IDbHelper
    {
        SqlConnection GetConnection();
    }
}

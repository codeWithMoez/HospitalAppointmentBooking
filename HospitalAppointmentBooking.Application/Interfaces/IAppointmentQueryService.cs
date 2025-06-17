using HospitalAppointmentBooking.Application.DTOs;

namespace HospitalAppointmentBooking.Application.Interfaces
{
    public interface IAppointmentQueryService
    {
        List<AppointmentDetailDto> GetAllDetails();
        AppointmentDetailDto GetDetailById(int id);
    }
}


using HospitalAppointmentBooking.Application.Interfaces;
using HospitalAppointmentBooking.Application.Services;
using HospitalAppointmentBooking.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalAppointmentBooking.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServices<Doctor>, DoctorService>();
            services.AddScoped<IServices<Patient>, PatientService>();
            services.AddScoped<IServices<Appointment>, AppointmentService>();

            return services;
        }
    }
}

using HospitalAppointmentBooking.Application.Interfaces;
using HospitalAppointmentBooking.Domain.Entities;
using HospitalAppointmentBooking.Infrastructure.Data;
using HospitalAppointmentBooking.Infrastructure.Repository;
using HospitalAppointmentBooking.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointmentBooking.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            // DB
            services.AddScoped<IDbHelper, DbHelper>(); 

            // Repository
            services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IRepository<Patient>, PatientRepository>();
            services.AddScoped<IRepository<Appointment>, AppointmentRepository>();

            // Services
            services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();

            return services;
        }
    }
}

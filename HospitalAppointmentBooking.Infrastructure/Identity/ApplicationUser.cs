using Microsoft.AspNetCore.Identity;

namespace HospitalAppointmentBooking.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}

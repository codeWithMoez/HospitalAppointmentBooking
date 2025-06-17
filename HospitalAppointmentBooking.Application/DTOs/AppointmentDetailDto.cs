using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointmentBooking.Application.DTOs
{
    public class AppointmentDetailDto
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public string? Remarks { get; set; }

        public int DoctorId { get; set; }
        public string? DoctorName { get; set; } 
        public string? Specialization { get; set; } 

        public int PatientId { get; set; }
        public string? PatientName { get; set; } 
        public string? Email { get; set; } 
    }

}

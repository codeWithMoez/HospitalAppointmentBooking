using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointmentBooking.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }     // ForeignKey
        public int PatientId { get; set; }    // ForeignKey
        public DateTime Date { get; set; }
        public string? Remarks { get; set; }
    }
}

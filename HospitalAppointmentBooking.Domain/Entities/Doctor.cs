﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointmentBooking.Domain.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Specialization { get; set; }
    }
}

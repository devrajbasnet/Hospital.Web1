using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public string Number { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        //public string DoctorId { get; set; }
        //public ApplicationUser Doctor { get; set; }

        // Foreign key for Patient (required)
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }
    }
}

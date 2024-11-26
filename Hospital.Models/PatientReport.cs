using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hospital.Models
{
    //public class PatientReport
    //{
    //    //public PatientReport()
    //    //{
    //    //   List< PrescribeMedicine> PrescribeMedicine = new List<PrescribeMedicine>();
    //    //}
    //    public int Id { get; set; }
    //    public string Diagnose { get; set; }
    //    public string DoctorId { get; set; }
    //    public string PatientId { get; set; }
    //    public ApplicationUser PatientName { get; set; }
    //    public ApplicationUser DoctorName { get; set; }
    //    [NotMapped]
    //    public ICollection<PrescribeMedicine> PrescribeMedicine { get; set; } = new List<PrescribeMedicine>();
    //}
    public class PatientReport
    {
        public int Id { get; set; }
        public string Diagnose { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public string PatientName { get; set; } 
        public string DoctorName { get; set; }
        public ApplicationUser User { get; set; }
     
        [NotMapped]
        public ICollection<PrescribeMedicine> PrescribeMedicine { get; set; } = new List<PrescribeMedicine>();
    }

}
      
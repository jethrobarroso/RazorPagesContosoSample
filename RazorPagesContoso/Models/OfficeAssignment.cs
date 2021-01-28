using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesContoso.Models
{
    public class OfficeAssignment
    {
        // EF Core can't automatically recognize InstructorID as the PK of OfficeAssignment 
        // because InstructorID doesn't follow the ID or classnameID naming convention. 
        // Therefore, the Key attribute is used to identify InstructorID as the PK:
        [Key]
        public int InstructorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesContoso.Models
{
    /*
     * Data models start out simple and grow. Join tables without payload (PJTs) frequently evolve to include payload. 
     * By starting with a descriptive entity name, the name doesn't need to change when the join table changes. 
     * Ideally, the join entity would have its own natural (possibly single word) name in the business domain. 
     * For example, Books and Customers could be linked with a join entity called Ratings. For the Instructor-to-Courses many-to-many relationship, CourseAssignment is preferred over CourseInstructor.
     */
    public class CourseAssignment
    {
        public int InstructorID { get; set; }
        public Instructor Instructor { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}

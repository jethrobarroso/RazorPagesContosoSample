using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesContoso.Data;
using RazorPagesContoso.Models;
using RazorPagesContoso.Models.SchoolViewModels;

namespace RazorPagesContoso.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesContoso.Data.SchoolContext _context;

        public IndexModel(RazorPagesContoso.Data.SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData();
            InstructorData.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                // Suppose users rarely want to see enrollments in a course. 
                // In that case, an optimization would be to only load the enrollment data if it's requested. 
                // Explicit loading is used in the last if statement.
                //.Include(i => i.CourseAssignments)
                //    .ThenInclude(i => i.Course
                //        .ThenInclude(i => i.Enrollments)
                //            .ThenInclude(i => i.Student)
                //.AsNoTracking() // Explicit loading requires the entity to be tracked.
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                InstructorID = id.Value;
                // Instructor instructor = InstructorData.Instructors
                //    .Where(i => i.ID == id.Value).Single();
                // Personal preference in using either preceding or below
                Instructor instructor = InstructorData.Instructors.Single(i => i.ID == id.Value);
                InstructorData.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                var selectedCourse = InstructorData.Courses
                    .Where(x => x.CourseID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                InstructorData.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}

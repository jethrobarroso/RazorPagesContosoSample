using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesContoso.Data;
using RazorPagesContoso.Models;

namespace RazorPagesContoso.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesContoso.Data.SchoolContext _context;

        public EditModel(RazorPagesContoso.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var studentToUpdate = await _context.Students.FindAsync(id);

            if (studentToUpdate == null)
            {
                return NotFound();
            }

            // Aside from using Viewmodel to prevent overposting, you can use TryUpdateModel 
            // to choose which properties are bound to the form
            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                nameof(Student), // Searches the prefix on the name attribute eg. Student.LastName
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}

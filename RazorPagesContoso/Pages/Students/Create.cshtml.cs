using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesContoso.Data;
using RazorPagesContoso.Models;
using RazorPagesContoso.ViewModels;

namespace RazorPagesContoso.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesContoso.Data.SchoolContext _context;

        public CreateModel(RazorPagesContoso.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StudentVM StudentVM { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Student());

            // This will try to match values from the StudentVM and apply it to the entity's properties
            entry.CurrentValues.SetValues(StudentVM);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}

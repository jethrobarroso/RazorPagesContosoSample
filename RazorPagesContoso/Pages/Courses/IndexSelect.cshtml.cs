using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesContoso.Data;
using RazorPagesContoso.ViewModels;

namespace RazorPagesContoso.Pages.Courses
{
    public class IndexSelectModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexSelectModel(SchoolContext context)
        {
            _context = context;
        }

        #region snippet_RevisedIndexMethod
        public IList<CourseViewModel> CourseVM { get; set; }

        public async Task OnGetAsync()
        {
            CourseVM = await _context.Courses
                    .Select(p => new CourseViewModel
                    {
                        CourseID = p.CourseID,
                        Title = p.Title,
                        Credits = p.Credits,
                        DepartmentName = p.Department.Name
                    }).ToListAsync();
        }
        #endregion
    }
}

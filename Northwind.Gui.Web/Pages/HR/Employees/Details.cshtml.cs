﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Northwind.Entities.Models;

namespace Northwind.Gui.Web.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly Northwind.Entities.Models.NorthwindContext _context;

        public DetailsModel(Northwind.Entities.Models.NorthwindContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                .Include(e => e.ReportsToNavigation).FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
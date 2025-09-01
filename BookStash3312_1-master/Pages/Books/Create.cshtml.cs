using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStash3312;
using BookStash3312.Models;

namespace BookStash3312.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStash3312.Models.BookContext _context;

        public CreateModel(BookStash3312.Models.BookContext context)
        {
            _context = context;
        }

        

        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
    
        public Book Book { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

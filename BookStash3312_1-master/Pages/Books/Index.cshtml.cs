using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStash3312;
using BookStash3312.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStash3312.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookStash3312.Models.BookContext _context;

        public IndexModel(BookStash3312.Models.BookContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;
        public IList<Review> Reviews {get;set;} = new List<Review>();

        [BindProperty(SupportsGet = true)]
        public int PageNum {get; set;} = 1;
        public int PageSize {get; set;} = 10;

        [BindProperty(SupportsGet = true)]
        public string? CurrentSort {get; set;} = string.Empty;
        public SelectList SortList {get; set;} = default!;

        public async Task OnGetAsync()
        {
            
            IQueryable<Book> query = _context.Books.Include(b => b.Reviews);
            {
                List<SelectListItem> sortItems = new List<SelectListItem> {
                    new SelectListItem {Text = "Rating Ascending", Value = "rate_asc"},
                    new SelectListItem {Text = "Rating Descending", Value = "rate_desc"}
                };
                SortList = new SelectList(sortItems, "Value", "Text", CurrentSort);

                switch (CurrentSort)
                {
                    case "rate_asc":
                        query = query.OrderBy(b => b.Reviews.Average(r => r.Rating));
                        break;
                    case "rate_desc":
                        query = query.OrderByDescending(b => b.Reviews.Average(r => r.Rating));
                        break;
                }

                Book = await query.Skip((PageNum -1)*PageSize).Take(PageSize).ToListAsync();
                Reviews = await _context.Reviews.ToListAsync();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStash3312.Models;
using System.Threading.Tasks;

namespace BookStash3312.Pages.Reviews
{
    public class CreateReviewModel : PageModel
    {
        private readonly BookStash3312.Models.BookContext _context;

        public IList<Review> Reviews {get;set;} = default!;
        public SelectList? BookList {get;set;}

        [BindProperty]
        public Review Review {get;set;}

        public CreateReviewModel(BookStash3312.Models.BookContext context)
        {
            _context = context;
        }

        

        public IActionResult OnGet()
        {
            PopulateBookList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateBookList();
                return Page();
            }

            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void PopulateBookList()
        {
            var books = _context.Books;
            BookList = new SelectList(books, "BookId", "Title");
        }
    }
}

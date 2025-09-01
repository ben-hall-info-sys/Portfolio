using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BookStash3312
{
    public class Review
    {
        public int ReviewID {get;set;} //PK

        [BindProperty]
        [Range(1,5)]
        [Required]
        public int Rating {get;set;}

        [BindProperty]
        public string ReviewText {get;set;} = string.Empty;

        public int BookID {get;set;} //FK
        public Book? Book {get;set;}
                
    }
}
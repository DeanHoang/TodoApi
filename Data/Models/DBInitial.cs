using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Data;
using TodoApi.Data.Models;

namespace TodoApi.Data.Models
{
    public class DBInitial
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookContext>();

                if (!context.Books.Any()) 
                {
                    context.Books.AddRange(
                    new Book()
                    {
                        Title = "1st Book Title",
                        Description = "1st Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now,
                        Rate = 4,
                        Genre = "Math",
                        Author = "1st Author",
                        CoverUrl = "https//...",
                        DateAdded = DateTime.Now

                    },
                    new Book()
                    {
                        Title = "2nd Book Title",
                        Description = "2nd Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now,
                        Rate = 4,
                        Genre = "Math",
                        Author = "2nd Author",
                        CoverUrl = "https//...",
                        DateAdded = DateTime.Now

                    },
                    new Book()
                    {
                        Title = "3rd Book Title",
                        Description = "3rd Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now,
                        Rate = 4,
                        Genre = "Math",
                        Author = "3rd Author",
                        CoverUrl = "https//...",
                        DateAdded = DateTime.Now

                    });
                }
                context.SaveChanges();
            }
        }
    }
}

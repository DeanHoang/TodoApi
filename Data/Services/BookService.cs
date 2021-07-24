using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Data.Models;
using TodoApi.Data.ViewModels;

namespace TodoApi.Data.Services
{
    public class BookService
    {
        public BookContext _context;
        public BookService(BookContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

        }
        public List<Book> GetAllBooks()
        {
           return _context.Books.ToList();
        }
        public Book GetBookById(int id)
        {
            var _book = _context.Books.FirstOrDefault(o => o.Id == id);
            return _book;
        }
        public Book UpdateBookById(int id,BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(o => o.Id == id);
            if (_book != null) {
                _book.Description = book.Description;
                _book.Title = book.Title;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.DateRead;
                _book.Author = book.Author;
                _book.CoverUrl = book.CoverUrl;
                _book.Genre = book.Genre;
                _book.Rate = book.Rate;

                _context.SaveChanges(); }
            return _book;
        }
        public void DeleteBook(int id)
        {
            var _book = _context.Books.FirstOrDefault(o => o.Id == id);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Services;
using TodoApi.Data.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService;
        // GET: api/<BooksController>
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public IActionResult GetAllBooks()
        {
            return Ok(_bookService.GetAllBooks());
            
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        public IActionResult GetBookById(int id)
        {
            return Ok(_bookService.GetBookById(id));
            
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookService.AddBook(book);
            return Ok();
        }
        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] BookVM book)
        //{
        //    Ok(_bookService.UpdateBookById(id, book));
        //}
        public IActionResult UpdateBookById(int id, BookVM book)
        {
            return Ok(_bookService.UpdateBookById(id, book));
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
           return Ok();
        }
    }
}

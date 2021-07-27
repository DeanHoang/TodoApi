using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data1;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PublishersController : ControllerBase
    {
        private readonly BookStoresDBContext _context;
        public PublishersController(BookStoresDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var pubs = _context.Publishers.ToList();
            return Ok(pubs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var _pub = _context.Publishers.Include(pub => pub.Books)
                                                .ThenInclude(book => book.Sales)
                                            .Include(pub => pub.Users)
                                              .FirstOrDefault(o => o.PubId == id);
                                           
            if (_pub != null) return Ok(_pub);
            else return NotFound("khong co");
        }

        [HttpPost]
        public IActionResult Add(Publisher pub)
        {
            _context.Publishers.Add(pub);
            _context.SaveChanges();
            return Ok(_context.Publishers.ToList());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, Publisher pub)
        {
            var _pub = _context.Publishers.FirstOrDefault(o => o.PubId == id);
            if (_pub != null)
            {
                _pub.PublisherName = pub.PublisherName;
                _pub.State = pub.State;
                _pub.Users = pub.Users;
                _pub.Books = pub.Books;
                _pub.City = pub.City;
                _pub.Country = pub.Country;
                _context.SaveChanges();
                return Ok(_pub);
            }
            return NotFound("Khong co Pub phu hop");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _pub = _context.Publishers.FirstOrDefault(o => o.PubId == id);
            if (_pub != null)
            {
                _context.Publishers.Remove(_pub);
                return Ok(_context.Publishers.ToList());
            }
            return NotFound("Khong tim thay");
        }
    }
}

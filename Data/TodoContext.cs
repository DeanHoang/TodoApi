using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Models;

namespace TodoApi.Data
    {
        public class TodoContext: DbContext
        {
            public TodoContext(DbContextOptions<TodoContext> options)
                : base(options)
            {

            }
            public DbSet<Todoitem> Todoitems { get; set; }
        }
    }

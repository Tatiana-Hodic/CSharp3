using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

namespace ToDoList.Persistence
{
    public class ToDoItemsContext : DbContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoItemsContext(DbContextOptions<ToDoItemsContext> options)
        {
            
        }
    }
}

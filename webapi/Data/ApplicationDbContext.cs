using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Produto> Produtos {get;set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {

        }
        
    }
}
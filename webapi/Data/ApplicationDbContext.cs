using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CasaDeShow> CasasDeShow {get;set;}
        public DbSet<Eventos> Eventos {get;set;}
        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet<Ingresso> Ingressos {get;set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {

        }
        
    }
}
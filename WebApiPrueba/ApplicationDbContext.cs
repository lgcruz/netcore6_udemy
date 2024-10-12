using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiPrueba.Entidades;

namespace WebApiPrueba
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

        }
        public DbSet<Autor> Autores { get; set; }      
        public DbSet<Libro> Libro { get; set; }
    }
}
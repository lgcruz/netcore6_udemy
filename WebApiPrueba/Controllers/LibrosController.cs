using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiPrueba.Entidades;
using Microsoft.EntityFrameworkCore;

namespace WebApiPrueba.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> GetById(int id)
        {
            var exist = await context.Libro.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null) {
                return NotFound("El libro no existe");
            }
            return exist;
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> Post(Libro libro)
        {
            var existeAutor = await context.Autores.FindAsync(libro.AutorId);
            if (existeAutor == null) {
                return BadRequest($"El autor del libro no existe con id: {libro.AutorId}");
            }
            libro.Autor = existeAutor;
            context.Libro.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
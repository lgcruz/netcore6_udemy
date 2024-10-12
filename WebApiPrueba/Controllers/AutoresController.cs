using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiPrueba.Entidades;
using Microsoft.EntityFrameworkCore;
using WebApiPrueba.Services;

namespace WebApiPrueba.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(
            ApplicationDbContext context, 
            IService service,
            ServiceTransient serviceTransient, 
            ServiceScoped serviceScoped, 
            ServiceSingleton serviceSingleton,
            ILogger<AutoresController> logger
        )
        {
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.context = context;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            logger.LogInformation("Consultando todos los autores");
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("GUID")]
        public ActionResult ObtenerGuids() {
            return Ok(new {
                AutoresController_Transient = serviceTransient.Guid,
                ServicioA_Transient = service.ObtenerTransient(),
                AutoresController_Scoped = serviceScoped.Guid,
                ServicioA_Scoped = service.ObtenerScoped(),
                AutoresController_Singleton = serviceSingleton.Guid,
                ServicioA_Singleton = service.ObtenerSingleton(),
            });
        }


        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            var existeNombre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (existeNombre) {
                return BadRequest($"Ya existe el nombre de autor {autor.Nombre}");
            }
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (autor.Id != id) {
                return BadRequest("El id del autor no coincide con el id de la url");
            }

            var checkAutor = await context.Autores.FindAsync(id);

            if (checkAutor == null) {
                return NotFound("El autor no existe");
            }
            checkAutor.Nombre = autor.Nombre;
            context.Update(checkAutor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.FindAsync(id);

            if (existe == null) {
                return NotFound("El autor no existe");
            }
            context.Remove(existe);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Veterinaria_Equipo_GuzDiaz.controller
{
    [ApiController]
    [Route("Veterinaria")]
    public class MascotasController : ControllerBase
    {
        [HttpPost("Mascotas")]
        public IActionResult registarNuevaMascota()
        {
            return Ok();
        }
        [HttpGet("Mascotas")]
        public IActionResult ObtenerTodasMascotas()
        {
            return Ok();
        }
        [HttpGet("Mascotas/{id}")]
        public IActionResult obtenerMascota([FromRoute] string id)
        {
            return Ok();
        }
        [HttpPut("Mascotas/{id}")]
        public IActionResult actualizarMascota([FromRoute]string id)
        {
            return Ok();
        }
        [HttpDelete("Mascotas/{id}")]
        public IActionResult eliminarMascota([FromRoute] string id)
        {
            return Ok();
        }    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Veterinaria_Equipo_GuzDiaz.controller
{
    [ApiController]
    [Route("veterinaria/")]
    public class ServiciosController : ControllerBase
    {
        [HttpPost("servicios")]
        public IActionResult registarNuevoServicio()
        {
            return Ok();
        }
        [HttpGet("servicios")]
        public IActionResult ObtenerTodosServicios()
        {
            return Ok();
        }
        [HttpGet("servicios/{id}")]
        public IActionResult obtenerServico([FromRoute] string id)
        {
            return Ok();
        }
        [HttpPut("servicios/{id}")]
        public IActionResult actualizarServicio([FromRoute]string id)
        {
            return Ok();
        }
        [HttpDelete("servicios/{id}")]
        public IActionResult eliminarServicio([FromRoute] string id)
        {
            return Ok();
        }    
    }
}
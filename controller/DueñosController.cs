using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Veterinaria_Equipo_GuzDiaz.controller
{
    [ApiController]
    [Route("veterinaria/")]
    public class DueñosController : ControllerBase
    {
        [HttpPost("dueños")]
        public IActionResult registarNuevoDueño()
        {
            return Ok();
        }
        [HttpGet("dueños")]
        public IActionResult ObtenerTododDueños()
        {
            return Ok();
        }
        [HttpGet("dueños/{id}")]
        public IActionResult obtenerDueño([FromRoute] string id)
        {
            return Ok();
        }
        [HttpPut("dueños/{id}")]
        public IActionResult actualizarDueño([FromRoute] string id)
        {
            return Ok();
        }
        [HttpDelete("dueños/{id}")]
        public IActionResult eliminarDueño([FromRoute] string id)
        {
            return Ok();
        }
        //*** controladores del dueño
        [HttpGet("/dueño/{id}")]
        public IActionResult obtenerInfoPersonal([FromRoute] string id)
        {
            return Ok();
        }
        [HttpPut("/dueño/{id}")]
        public IActionResult ActualizarInfoPersonal([FromRoute] string id)
        {
            return Ok();
        }

        //* controladores para las mascotas del dueño
        [HttpGet("/dueño/{id}/mascotas")]
        public IActionResult obtenerMascotas([FromRoute] string id)
        {
            return Ok();
        }
        [HttpPost("/dueño/{id}/mascota")]
        public IActionResult agregarMascota([FromRoute] string id)
        {
            return Ok();
        }
        [HttpGet("/dueño/{id}/mascota/{mascotaID}")]
        public IActionResult obtenerInfoPersonal([FromRoute] string id, [FromRoute] string mascotaID)
        {
            return Ok();
        }
        [HttpPut("/dueño/{id}/mascota/{mascotaId}")]
        public IActionResult ActualizarInfoMascota([FromRoute] string id, [FromRoute] string mascotaID)
        {
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veterinaria_Equipo_GuzDiaz.DTOs;
using Veterinaria_Equipo_GuzDiaz.services;

namespace Veterinaria_Equipo_GuzDiaz.controller
{
    [ApiController]
    [Route("veterinaria/")]
    public class DueñosController : ControllerBase
    {
        private readonly DueñoService _service;
        public DueñosController(DueñoService service)
        {
            _service = service;
        }

        [HttpPost("dueños")]
        public IActionResult registarNuevoDueño([FromBody] DueñoCreateDto data)
        {
            try
            {
                var respone = _service.registrarNuevoDueño(data);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dueños")]
        public IActionResult ObtenerTodosDueños()
        {
            try
            {
                var dueños = _service.ObtenerTodosDueños();
                return Ok(dueños);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dueños/{id}")]
        public IActionResult obtenerDueño([FromRoute] string id)
        {
            try
            {
                var dueño = _service.obtenerDueño(id);
                if (dueño == null)
                {
                    return BadRequest("Usuario no encontrado");
                }
                return Ok(dueño);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
                //return BadRequest(ex);
            }
        }

        [HttpPut("dueños/{id}")]
        public IActionResult actualizarDueño([FromRoute] string id, DueñoUpdateDto dueñoUp)
        {
            try
            {
                var dueño = _service.actualizarDueño(id, dueñoUp);
                return Ok(dueño);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("dueños/{id}")]
        public IActionResult eliminarDueño([FromRoute] string id)
        {
            try
            {
                var dueño = _service.eliminarDueño(id);
                return Ok(dueño);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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
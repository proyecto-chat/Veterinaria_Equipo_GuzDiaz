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
                return BadRequest(new { error = ex.Message });
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

        [HttpGet("dueños/buscar")]
        public IActionResult obtenerDueño([FromQuery] string id)
        {
            try
            {
                var dueño = _service.obtenerDueño(id);
                if (dueño == null)
                {
                    return NotFound("Usuario no encontrado");
                }
                return Ok(dueño);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("dueños/actualizar")]
        public IActionResult actualizarDueño([FromQuery] string id, [FromBody] DueñoUpdateDto dueñoUp)
        {
            try
            {
                var actualizado = _service.actualizarDueño(id, dueñoUp);
                if (actualizado)
                {
                    return Ok(new { mensaje = "Dueño actualizado correctamente" });
                }
                else
                {
                    return NotFound(new { error = "Dueño no encontrado" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("dueños/eliminar")]
        public IActionResult eliminarDueño([FromQuery] string id)
        {
            try
            {
                var eliminado = _service.eliminarDueño(id);
                if (eliminado)
                {
                    return Ok(new { mensaje = "Dueño eliminado correctamente" });
                }
                else
                {
                    return NotFound(new { error = "Dueño no encontrado" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("dueños/mascotas")]
        public IActionResult obtenerMascotas([FromQuery] string dni)
        {
            try
            {
                var mascotas = _service.obtenerMascotasDueño(dni);
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
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
    public class MascotasController : ControllerBase
    {
        private readonly MascotaService _service;

        public MascotasController(MascotaService service)
        {
            _service = service;
        }

        [HttpPost("mascotas")]
        public IActionResult registarNuevaMascota([FromBody] MascotaCreateDto info)
        {
            try
            {
                var mascota = _service.registrarNuevaMascota(info);
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("mascotas")]
        public IActionResult ObtenerTodasMascotas()
        {
            try
            {
                var mascotas = _service.obtenerTodasMascotas();
                return Ok(mascotas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("mascotas/buscar")]
        public IActionResult obtenerMascota([FromQuery] string id)
        {
            try
            {
                var mascota = _service.obtenerMascota(id);
                if (mascota == null)
                {
                    return NotFound("Mascota no encontrada");
                }
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("mascotas/actualizar")]
        public IActionResult actualizarMascota([FromQuery] string id, [FromBody] MascotaUpdateDto mascotaUp)
        {
            try
            {
                var actualizada = _service.actualizarMascota(id, mascotaUp);
                if (actualizada)
                {
                    return Ok(new { mensaje = "Mascota actualizada correctamente" });
                }
                else
                {
                    return NotFound("Mascota no encontrada");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("mascotas/eliminar")]
        public IActionResult eliminarMascota([FromQuery] string id)
        {
            try
            {
                var eliminado = _service.eliminarMascota(id);
                if (eliminado)
                {
                    return Ok(new { mensaje = "Mascota eliminada correctamente" });
                }
                else
                {
                    return NotFound(new { error = "Mascota no encontrada" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
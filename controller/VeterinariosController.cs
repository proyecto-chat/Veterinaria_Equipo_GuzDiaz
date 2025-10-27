using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;
using Veterinaria_Equipo_GuzDiaz.services;

namespace Veterinaria_Equipo_GuzDiaz.controller
{
    [ApiController]
    [Route("veterinaria/")]
    public class VeterinariosController : ControllerBase
    {

        private readonly VeterinarioService _service;
        private readonly RegistroClinicoService _registroClinicoService;
        public VeterinariosController(VeterinarioService service, RegistroClinicoService registroClinicoService)
        {
            _service = service;
            _registroClinicoService = registroClinicoService;
        }

        [HttpPost("veterinarios")]
        public IActionResult registrarVeterinario([FromBody] VeterinarioCreateDto data)
        {
            try
            {
                var response = _service.register(data);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("veterinarios")]
        public IActionResult obtenerVeteriarios()
        {
            try
            {
                var response = _service.obtenerVeterinarios();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("veterinarios/{id}")]
        public IActionResult obtenerVeterinario([FromRoute] string id)
        {
            try
            {
                var response = _service.obtenerVeterinarioMatricula(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut("veterinarios/{id}")]
        public IActionResult actualizarVeterinario([FromRoute] string id, [FromBody] VeterinarioUpdateDto newInfo)
        {
            try
            {
                var response = _service.actualizarInfoVeterinario(newInfo, id);
                return Ok("Informacion actualizada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("veterinarios/{id}")]
        public IActionResult eliminarVeterinario([FromRoute] string id)
        {
            try
            {
                var response = _service.eliminarVeterinario(id);
                return Ok("Veterinario eliminado con exito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("veterinarios/{id}/registros-clinicos")]
        public IActionResult obtenerRegistrosClinicosPorVeterinario([FromRoute] string id)
        {
            try
            {
                var response = _registroClinicoService.obtenerHistorialClinicoPorMascota(Guid.Parse(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("veterinarios/con-mas-registros")]
        public IActionResult obtenerVeterinarioConMasRegistrosClinicos([FromQuery] DateTime desdeFecha, [FromQuery] DateTime hastaFecha)
        {
            try
            {
                var response = _registroClinicoService.ObtenerVeterinarioConMasRegistrosClinicos(desdeFecha, hastaFecha);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("veterinarios/asignar-vacunas")]
        public IActionResult asignarVacunasARegistroClinico([FromQuery] string idMascota, [FromBody] List<string> vacunas,[FromQuery] DateTime fechaAplicacion)
        {
            try
            {
                var response = _service.asignarVacunas(vacunas, idMascota,fechaAplicacion);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
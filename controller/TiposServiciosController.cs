using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;
using Veterinaria_Equipo_GuzDiaz.services;

namespace Veterinaria_Equipo_GuzDiaz.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposServiciosController : ControllerBase
    {
        private readonly TiposServicioService _service;
        private readonly EspecialidadesService _especialidadesService;
        private readonly VacunasService _serviceVacunas;
        public TiposServiciosController(TiposServicioService service, EspecialidadesService especialidadesService, VacunasService serviceVacunas)
        {
            _service = service;
            _especialidadesService = especialidadesService;
            _serviceVacunas = serviceVacunas;
        }
        [HttpPost("/tiposdeservicio/crear")]
        public IActionResult CrearTipoServicio([FromBody] TiposServiciosCreatDto data)
        {
            try
            {
                var nuevoTipoServicio = _service.crearTipoServicio(data.NombreServicio, data.Descripcion);
                return Ok(nuevoTipoServicio);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { mensaje = "Error al crear el tipo de servicio.", detalle = ex.Message });
            }
        }

        [HttpGet("/tiposdeservicio/obtener")]
        public IActionResult ObtenerTiposServicio()
        {
            try
            {
                var tiposServicio = _service.obtenerTiposServicio();
                return Ok(tiposServicio);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { mensaje = "Error al obtener los tipos de servicio.", detalle = ex.Message });
            }
        }

        [HttpPost("/especialidades/crear")]
        public IActionResult CrearEspecialidad([FromBody] EspecialidadesCreateDto data)
        {
            try
            {
                var nuevaEspecialidad = _especialidadesService.crearEspecialidad(data.Nombre, data.Descripcion);
                return Ok(nuevaEspecialidad);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { mensaje = "Error al crear la especialidad.", detalle = ex.Message });
            }
        }

        [HttpGet("/especialidades/obtener")]
        public IActionResult ObtenerEspecialidades()
        {
            try
            {
                var especialidades = _especialidadesService.obtenerEspecialidades();
                return Ok(especialidades);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { mensaje = "Error al obtener las especialidades.", detalle = ex.Message });
            }
        }

        [HttpPost("/vacunas/registrar")]
        public IActionResult RegistrarVacuna([FromBody] Vacuna vacuna)
        {
            try
            {
                var vacunaRegistrada = _serviceVacunas.Register(vacuna);
                return Ok(vacunaRegistrada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al registrar la vacuna.", detalle = ex.Message });
            }
        }
        [HttpGet("/vacunas/obtener")]
        public IActionResult ObtenerVacunas()
        {
            try
            {
                var vacunas = _serviceVacunas.GetAllVaccines();
                return Ok(vacunas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener las vacunas.", detalle = ex.Message });
            }
        }

    }
}
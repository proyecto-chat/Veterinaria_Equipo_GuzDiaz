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
    public class ServiciosController : ControllerBase
    {
        private readonly ServicioService _service;
        public ServiciosController(ServicioService servicio)
        {
            _service = servicio;
        }

        [HttpPost("servicios")]
        public IActionResult registarNuevoServicio([FromBody] ServicioMedicoCreateDto data)
        {
            try
            {
                var resposne = _service.crearServicioMedido(data);
                return Ok(resposne);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("servicios")]
        public IActionResult ObtenerTodosServicios()
        {
            try
            {
                var response = _service.obtenerServicios();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("servicios/{id}")]
        public IActionResult obtenerServico([FromRoute] string id)
        {
            try
            {
                var response = _service.obtnerServicio(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("servicios/{id}")]
        public IActionResult actualizarServicio([FromRoute] string id)
        {
            return Ok();
        }
        
        [HttpDelete("servicios/{id}")]
        public IActionResult eliminarServicio([FromRoute] string id)
        {
            try
            {
                var response = _service.eliminarServicio(id);
                return Ok("servicio elimindo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    
    }
}
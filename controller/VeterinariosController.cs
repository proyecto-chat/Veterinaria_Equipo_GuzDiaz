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
        public VeterinariosController(VeterinarioService service)
        {
            _service = service;
        }

        [HttpPost("veterinarios")]
        public IActionResult registrarVeterinario([FromBody]VeterinarioCreateDto data)
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
        public IActionResult actualizarVeterinario([FromRoute] string id,[FromBody] VeterinarioUpdateDto newInfo)
        {
            try
            {
                var response = _service.actualizarInfoVeterinario(newInfo);
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


    }
}
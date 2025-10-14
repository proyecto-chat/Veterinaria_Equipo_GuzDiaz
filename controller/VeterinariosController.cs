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
            return Ok();
        }

        [HttpGet("veterinarios/{id}")]
        public IActionResult obtenerVeterinario([FromRoute] string id)
        {
            return Ok();
        }

        [HttpPut("veterinarios/{id}")]
        public IActionResult actualizarVeterinario([FromRoute] string id)
        {
            return Ok();
        }

        [HttpDelete("veterinarios/{id}")]
        public IActionResult eliminarVeterinario([FromRoute] string id)
        {
            return Ok();
        }


    }
}
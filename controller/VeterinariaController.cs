using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria.controller
{
    [ApiController]
    [Route("Veterinario/")]
    public class VeterinariaController : ControllerBase
    {

        [HttpPost("nuevaMascotas")]
        public IActionResult RegistrarMascota([FromBody] RequestRegisterPet infoPet)
        {

            return Ok();
        }

        [HttpPost("nuevaReservacion")]
        public IActionResult CreateResevation([FromBody] RequestCrearServicio data)
        {
            return Ok();
        }

        [HttpPost("FinalizarReservaciones")]
        public IActionResult FinalzarReservaciones([FromBody] string idReservaciones)
        {
            return Ok();
        }

    }
}
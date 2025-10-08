using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Data.Models;

namespace Veterinaria.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinariaController : ControllerBase
    {
        [HttpGet("/dueños")]
        public IActionResult getPeople()
        {
        
            return Ok("");
        }
    }
}
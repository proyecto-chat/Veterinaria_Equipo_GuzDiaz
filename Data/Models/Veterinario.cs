using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Data.Models
{
    public class Veterinario
    {
        public Persona persona { get; set; }
        public Especialidades especialidades { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Data.Models
{
    public class Veterinario : Persona
    {
        public string Matricula { get; set; }
        public List<Especialidades> especialidades { get; set; }
        public List<ServicioMedico> servicioMedicos { get; set; }
    }
}
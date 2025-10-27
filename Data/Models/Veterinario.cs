using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria.Data.Models
{
    public class Veterinario : Persona
    {
        public string Matricula { get; set; } = string.Empty;

        public List<Especialidades> especialidades { get; set; } = new();
        [BsonRef("serviciomedico")]
        public List<ServicioMedico> servicioMedicos { get; set; } = new();
    }
}
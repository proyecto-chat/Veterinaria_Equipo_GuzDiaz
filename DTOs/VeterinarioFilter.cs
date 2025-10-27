using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class VeterinarioFilter
    {
        public string Matricula { get; set; }
        public List<Especialidades> Especialidades { get; set; } = new List<Especialidades>();
        public int ServicioMedicos { get; set; } = 0;
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
    }
}
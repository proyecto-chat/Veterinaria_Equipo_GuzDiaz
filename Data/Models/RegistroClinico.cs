using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class RegistroClinico
    {
        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public ServicioMedico ServicioRelacionado { get; set; }
    }
}
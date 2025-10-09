using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Data.Models
{
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }

    }

}
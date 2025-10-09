using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class Perro : Mascota
    {
        public string Raza { get; set; }
        public string NivelAdiestramiento { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class Gato : Mascota
    {
        public string ColorPelaje { get; set; }
        public bool EsIndoor { get; set; }
    }
}
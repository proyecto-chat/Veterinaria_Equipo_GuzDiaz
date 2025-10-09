using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class Reptil : Mascota
    {
        public string Tipo { get; set; }
        public bool EsVeneno { get; set; }
    }
}
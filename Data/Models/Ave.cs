using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class Ave : Mascota
    {
        public string Especie { get; set; }
        public bool PuedeHablar { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class EspecieCreateDto
    {
        public string NombreEspecie { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
    }
}
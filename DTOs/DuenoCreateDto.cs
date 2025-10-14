using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    // DTO para crear o actualizar dueño
    public class DueñoCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }

}
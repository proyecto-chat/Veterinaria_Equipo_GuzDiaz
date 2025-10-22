using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    // DTO para devolver datos del dueño
    public class DueñoReadDto
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public List<MascotaReadDto> Mascotas { get; set; } = new();
    }

}
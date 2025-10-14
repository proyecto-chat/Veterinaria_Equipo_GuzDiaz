using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    // DTO para devolver datos de veterinario
    public class VeterinarioReadDto
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public List<EspecialidadReadDto> Especialidades { get; set; } = new();
    }

}
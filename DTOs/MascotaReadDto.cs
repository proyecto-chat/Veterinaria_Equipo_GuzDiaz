using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria_Equipo_GuzDiaz.services;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    // DTO para devolver mascota
    public class MascotaReadDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public float Peso { get; set; }
        public EspecieReadDto Especie { get; set; } = new();
        public List<VacunaReadDto> Vacunas { get; set; } = new();
    }

}
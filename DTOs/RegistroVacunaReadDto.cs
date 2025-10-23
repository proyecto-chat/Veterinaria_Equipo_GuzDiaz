using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria_Equipo_GuzDiaz.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class RegistroVacunaReadDto
    {
        public string nombreMascota { get; set; } = string.Empty;
        public List<Vacuna> vacunas { get; set; } = new();
    }
    
}
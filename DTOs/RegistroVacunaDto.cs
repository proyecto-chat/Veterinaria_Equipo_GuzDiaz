using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria_Equipo_GuzDiaz.services;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class RegistroVacunaDto
    {
        public string idMascota { get; set; }
        public string nombreMascota { get; set; }
        public List<VacunaReadDto> vacunas { get; set; }
    }
}
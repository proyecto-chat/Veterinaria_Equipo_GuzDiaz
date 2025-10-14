using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class RegistroClinicoCreateDto
    {
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;
        public Guid VeterinarioId { get; set; }
        public Guid MascotaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

    public class RegistroClinicoReadDto
    {
        public Guid Id { get; set; }
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public VeterinarioReadDto Veterinario { get; set; } = new();
        public MascotaReadDto Mascota { get; set; } = new();
    }

}
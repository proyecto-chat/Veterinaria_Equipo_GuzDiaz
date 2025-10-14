using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class ServicioMedicoReadDto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public float Costo { get; set; }
        public DateTime Fecha { get; set; }
        public VeterinarioReadDto Veterinario { get; set; } = new();
        public MascotaReadDto Mascota { get; set; } = new();
    }

}
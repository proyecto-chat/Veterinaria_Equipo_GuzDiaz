using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class ServicioMedicoCreateDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public float Costo { get; set; }
        public Guid VeterinarioId { get; set; }
        public Guid MascotaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
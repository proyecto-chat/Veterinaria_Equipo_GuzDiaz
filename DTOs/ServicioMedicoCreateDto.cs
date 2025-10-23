using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class ServicioMedicoCreateDto
    {
        public List<string> tiposServicio { get; set; } = new();
        public string Descripcion { get; set; } = string.Empty;
        public float Costo { get; set; }
        public DateTime FechaServicio { get; set; } = new();
        public string VeterinarioId { get; set; }
        public string MascotaId { get; set; }
    }
}
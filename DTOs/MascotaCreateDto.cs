using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    // DTO para crear o actualizar mascota
    public class MascotaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public float Peso { get; set; }
        //public Guid EspecieId { get; set; }
        public EspecieCreateDto Especie { get; set; } = new(); 
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Veterinaria.Data.Models
{
    public class ServicioMedico
    {
        public DateTime FechaServicio { get; set; } = new();
        public Veterinario veterinario { get; set; }
        public DetallesClinicos detallesClinicos { get; set; }
        public Mascota mascota { get; set; }
        public Dueño dueño { get; set; }
        public double costo { get; set; }
        public TiposDeServicio tiposDeServicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
    }
}
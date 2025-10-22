using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class Vacuna
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public bool EstaVencida()
        {
            return FechaAplicacion < DateTime.Now.AddYears(-1);
        }
    }
}
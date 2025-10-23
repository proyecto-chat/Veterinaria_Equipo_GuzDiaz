using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria_Equipo_GuzDiaz.Data.Models;

namespace Veterinaria.Data.Models
{
    public class Mascota
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public Especie Especie { get; set; }

        //[BsonRef("registroclinico")]
        public List<Guid> registroClinicos { get; set; } = new();
        //[BsonRef("dueños")]
        public string dueñoDni { get; set; } = string.Empty;
        public List<vacunasAplicadas> Vacunas { get; set; } = new();

        public List<Guid> GetHistorial()
        {
            return registroClinicos;
        }
    }

    public class vacunasAplicadas : Vacuna{
        public DateTime FechaAplicacion { get; set; }
        public bool EstaVencida()
        {
            return FechaAplicacion < DateTime.Now.AddYears(-1);
        }
    }

    public class Especie
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NombreEspecie { get; set; }
        public string Raza { get; set; } = string.Empty;
    }
}
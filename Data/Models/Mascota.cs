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
        public List<RegistroClinico> registroClinicos { get; set; } = new();
        //[BsonRef("dueños")]
        public Dueño dueño { get; set; }

        public List<RegistroClinico> GetHistorial()
        {
            return registroClinicos;
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
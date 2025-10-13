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
        public string Nombre { get; set; }
        [BsonId]
        public int Edad { get; set; }
        public float Peso { get; set; }
        public Especie Especie { get; set; }
        public List<RegistroClinico> registroClinicos { get; set; } = new();
        public Dueño dueño { get; set; }

        public List<RegistroClinico> GetHistorial()
        {
            return registroClinicos;
        }
    }

    public class Especie
    {
        public string NombreEpecie { get; set; }
        public string Raza { get; set; } = string.Empty;
    }
}
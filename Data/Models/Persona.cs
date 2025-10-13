using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria.Data.Models
{
    public class Persona
    {   
        [BsonId]
        public int id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }

    }

}
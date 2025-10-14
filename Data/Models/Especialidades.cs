using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria.Data.Models
{
    public class Especialidades
    {

        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
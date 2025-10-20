using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria.Data.Models
{
    public class Dueño:Persona
  {
    //[BsonRef("mascotas")]
    public List<Mascota> Mascotas { get; set; } = new List<Mascota>();
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Data.Models
{
    public class Dueño:Persona
  {
    public List<Mascota> Mascotas { get; set; }
  }
}
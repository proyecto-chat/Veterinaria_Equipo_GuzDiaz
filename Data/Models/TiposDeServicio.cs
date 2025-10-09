using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Data.Models
{
    public class TiposDeServicio
    {
        public int id { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        
        public void UpdateDescripcion(string newDescripcion)
        {
            this.Descripcion = newDescripcion;
        }

    }
}
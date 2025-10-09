using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria_Equipo_GuzDiaz.Data.Models;

namespace Veterinaria.Data.Models
{
    public class Mascota
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public List<RegistroClinico> registroClinicos { get; set; } = new();

        public List<RegistroClinico> GetHistorial()
        {
            return registroClinicos;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class HistorialClinico
    {
        public List<RegistroClinico> registros { get; set; }

        public void AgregarRegistro(RegistroClinico registro)
        {
            if (registro == null) throw new ArgumentNullException(nameof(registro));
            registros.Add(registro);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria_Equipo_GuzDiaz.Interfaces
{
    public interface IServiceMedico
    {
        public void CrearServicioMedico();
        public void FinalizarServicioMedico();
        public void CrearHistorial();
    }
}
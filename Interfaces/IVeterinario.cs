using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Interfaces
{
    public interface IVeterinario
    {
        public void CrearServicio();
        public void RegistrarMedico();
        public void RegistrarDueño();
        public void ObtenerHistorial();
        public void ActualizarMascota();
        public void EliminarMascotas();
    }
}
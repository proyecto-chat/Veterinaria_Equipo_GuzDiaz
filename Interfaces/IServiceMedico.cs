using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Interfaces
{
    public interface IServiceMedico
    {
        public void CrearServicioMedico(RequestCrearServicio newServicio);
        public void FinalizarServicioMedico(RequestFinalizarServico servicioInfo);
        public void CrearHistorial();
    }
}
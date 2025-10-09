using System;
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Interfaces;

namespace Veterinaria_Equipo_GuzDiaz.Services
{
    public class ServicioMedicoService : IServiceMedico
    {
        private const string DB_FILE = "examen.db";
        private readonly ILiteCollection<ServicioMedico> _servicioMedico;

        public ServicioMedicoService()
        {
            var db = new LiteDatabase(DB_FILE);
            _servicioMedico = db.GetCollection<ServicioMedico>("servicioMedico");
        }

        public void CrearHistorial()
        {
            throw new NotImplementedException();
        }

        public void CrearServicioMedico(RequestCrearServicio newServicio)
        {
            if (newServicio == null)
                throw new ArgumentNullException(nameof(newServicio));

            var servicio = new ServicioMedico
            {
                Fecha = DateTime.Now,
                Descripcion = newServicio.Descripcion,
                Detalles = newServicio.Detalles,
                Mascota = newServicio.Mascota,
                Veterinario = newServicio.Veterinario,
                FechaFinalizacion = null
            };

            _servicioMedico.Insert(servicio);
        }


        public void FinalizarServicioMedico(RequestFinalizarServico servicioInfo)
        {
            if (servicioInfo == null)
                throw new ArgumentNullException(nameof(servicioInfo));

            var servicio = _servicioMedico.FindById(servicioInfo.id);
            if (servicio == null)
                throw new Exception("Servicio médico no encontrado.");

            servicio.FechaFinalizacion = DateTime.Now;
            _servicioMedico.Update(servicio);
        }

    }

}

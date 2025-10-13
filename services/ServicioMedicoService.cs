using System;
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Interfaces;

namespace Veterinaria_Equipo_GuzDiaz.Services
{
    public class ServicioMedicoService : IServiceMedico
    {
        private const string DB_FILE = "examen.db";
        private readonly ILiteCollection<ServicioMedico> _servicioMedico;
        private readonly ILiteCollection<HistorialClinico> _historialClinico;
        private readonly ILiteCollection<RegistroClinico> _registroClinico;

        public ServicioMedicoService()
        {
            var db = new LiteDatabase(DB_FILE);
            _servicioMedico = db.GetCollection<ServicioMedico>("servicioMedico");
            _historialClinico = db.GetCollection<HistorialClinico>("historialClinico");
            _registroClinico = db.GetCollection<RegistroClinico>("registroClinico");
        }

        public void CrearHistorial(RequestCrearHistorialClinico historialClinico)
        {
            if (historialClinico == null) throw new ArgumentNullException(nameof(historialClinico));

            var servicioRelacionado = _servicioMedico.FindById(historialClinico.idSerivcioMedico);

            if (servicioRelacionado == null) throw new Exception("Error no existe el servicio indicado");

            var registroClinico = new RegistroClinico
            {
                Diagnostico = historialClinico.Diagnostico,
                Fecha = DateTime.Now,
                Tratamiento = historialClinico.Tratamiento,
                ServicioRelacionado = servicioRelacionado
            };
            _registroClinico.Insert(registroClinico);
            var historial = new HistorialClinico();
            historial.AgregarRegistro(registroClinico);
            _historialClinico.Insert(historial);
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

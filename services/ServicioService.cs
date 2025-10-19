using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class ServicioService
    {
        private readonly string DB_FILE = "examen.db";
        private readonly ILiteCollection<ServicioMedico> _servicioMedico;
        private readonly ILiteCollection<Mascota> _mascota;
        private readonly ILiteCollection<Veterinario> _veterinarios;

        public ServicioService()
        {
            var db = new LiteDatabase(DB_FILE);
            _servicioMedico = db.GetCollection<ServicioMedico>("serviciomedico");
            _mascota = db.GetCollection<Mascota>("mascotas");
            _veterinarios = db.GetCollection<Veterinario>("veterinarios");
        }

        public ServicioMedicoReadDto crearServicioMedido(ServicioMedicoCreateDto servicioMedico)
        {
            if (servicioMedico == null)
            {
                throw new Exception("La informacion no es correcta");
            }
            //TODO: buscar ala mascota
            var mascostaFound = _mascota.FindById(servicioMedico.MascotaId);
            if (mascostaFound == null)
            {
                throw new Exception("No se encontro la mascota");
            }
            //TODO: buscar al veterinario
            var veterinarioFound = _veterinarios.FindById(servicioMedico.VeterinarioId);
            if (veterinarioFound == null)
            {
                throw new Exception("No se encontro al veterinario");
            }
            var newServicioMedico = new ServicioMedico
            {
                Costo = servicioMedico.Costo,
                Descripcion = servicioMedico.Descripcion,
                Detalles = servicioMedico.Detalles,
                Fecha = DateTime.Now,
                Mascota = mascostaFound,
                Veterinario = veterinarioFound,
            };

            //! creamos el arreglo con las especialiodades del veterinarios
            var especialidadesDto = veterinarioFound.especialidades?
               .Select(e => new EspecialidadReadDto
               {
                   Id = e.Id,
                   Nombre = e.nombre,
                   Descripcion = e.Descripcion
               }).ToList() ?? new List<EspecialidadReadDto>();

            _servicioMedico.Insert(newServicioMedico);

            //? creamos el DTo servicio medico
            return new ServicioMedicoReadDto
            {
                Costo = newServicioMedico.Costo,
                Descripcion = newServicioMedico.Descripcion,
                Fecha = newServicioMedico.Fecha,
                Mascota = new MascotaReadDto
                {
                    Edad = mascostaFound.Edad,
                    Especie ={
                        NombreEspecie = mascostaFound.Nombre,
                        Raza = mascostaFound.Especie.Raza
                    },
                    Nombre = mascostaFound.Nombre,
                    Peso = mascostaFound.Peso
                },
                Veterinario =
                {
                    Matricula = veterinarioFound.Matricula,
                    NombreCompleto = $"{veterinarioFound.Nombre} {veterinarioFound.Apellido}",
                    Especialidades = especialidadesDto
                }


            };

        }

        public List<ServicioMedico> obtenerServicios()
        {
            var listServicios = _servicioMedico.FindAll().ToList();
            if (listServicios == null || listServicios.Any())
            {
                throw new Exception("No hay servicios registrador");
            }
            return listServicios;
        }

        public ServicioMedico obtnerServicio(string id)
        {
            var servicio = _servicioMedico.FindById(id);
            if (servicio == null)
            {
                throw new Exception("No se encontro la reservacion");
            }
            return servicio;
        }

        public bool eliminarServicio(String id)
        {
            var servicioEliminado = _servicioMedico.Delete(id);
            if (!servicioEliminado)
            {
                throw new Exception("No se lo eliminar el servicio medico");
            }
            return servicioEliminado;
        }
    
    
    }
}
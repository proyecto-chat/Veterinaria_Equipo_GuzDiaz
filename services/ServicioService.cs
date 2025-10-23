using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class ServicioService : ServicioGenerico<ServicioMedico>
    {
        private readonly string DB_FILE = "examen.db";
        private readonly ILiteCollection<Mascota> _mascota;
        private readonly ILiteCollection<Veterinario> _veterinarios;
        private readonly RegistroClinicoService _registroClinicoService;

        public ServicioService(RegistroClinicoService service ) : base("serviciomedico")
        {
            var db = new LiteDatabase(DB_FILE);
            _mascota = db.GetCollection<Mascota>("mascotas");
            _veterinarios = db.GetCollection<Veterinario>("veterinarios");
            _registroClinicoService = service;
        }

        public ServicioMedicoReadDto crearServicioMedido(ServicioMedicoCreateDto servicioMedico)
        {
            if (servicioMedico == null)
            {
                throw new Exception("La informacion no es correcta");
            }
            //** buscamos ala mascota
            var mascostaFound = _mascota.FindById(servicioMedico.MascotaId);
            if (mascostaFound == null)
            {
                throw new Exception("No se encontro la mascota");
            }
            //** buscamos al veterinario
            var veterinarioFound = _veterinarios.FindById(servicioMedico.VeterinarioId);
            if (veterinarioFound == null)
            {
                throw new Exception("No se encontro al veterinario");
            }
            //*creamos el servicio medico
            var newServicioMedico = new ServicioMedico
            {
                Costo = servicioMedico.Costo,
                Descripcion = servicioMedico.Descripcion,
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

            //?Guardamos el servicio medico
            Insert(newServicioMedico);
            //? Creamos el registro clinico
            _registroClinicoService.agregarRegistro(servicioMedico, veterinarioFound, mascostaFound);

            //? creamos el DTo servicio medico que nos indica la informacion que debemos de devolver
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


        //?Metodos para obtener los servicios medicos
        public List<ServicioMedico> obtenerServicios()
        {
            var listServicios = GetAll();
            if (listServicios == null || listServicios.Any())
            {
                throw new Exception("No hay servicios registrador");
            }
            return listServicios;
        }

        //?Metodo para obtener un servicio medico por su id
        public ServicioMedico obtnerServicio(string id)
        {
            var servicio = GetOne(S => S.Id == Guid.Parse(id));
            if (servicio == null)
            {
                throw new Exception("No se encontro la reservacion");
            }
            return servicio;
        }

        //?Metodod para eliminar un servicio medico
        public bool eliminarServicio(String id)
        {
            //?Le indicamos como debe de buscar al servicio medico que debe eliminar
            var servicioEliminado = Delete(S => S.Id == Guid.Parse(id));
            if (!servicioEliminado)
            {
                throw new Exception("No se lo eliminar el servicio medico");
            }
            return servicioEliminado;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.DB;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class ServicioService : ServicioGenerico<ServicioMedico>
    {
        private readonly ILiteCollection<Mascota> _mascota;
        private readonly ILiteCollection<Veterinario> _veterinarios;
        private readonly ILiteCollection<RegistroClinico> _registroClinicoService;
        private readonly ILiteCollection<TiposDeServicio> _tiposServicio;


        public ServicioService(LiteDatabase db) : base(db, "serviciomedico")
        {
            _mascota = db.GetCollection<Mascota>("mascotas");
            _veterinarios = db.GetCollection<Veterinario>("veterinarios");
            _registroClinicoService = db.GetCollection<RegistroClinico>("registroclinico");
            _tiposServicio = db.GetCollection<TiposDeServicio>("tiposdeservicio");
        }

        public ServicioMedicoReadDto crearServicioMedido(ServicioMedicoCreateDto servicioMedico)
        {
            if (servicioMedico == null)
            {
                throw new Exception("La informacion no es correcta");
            }
            //** buscamos ala mascota
            var mascostaFound = _mascota.Find(M => M.Id == Guid.Parse(servicioMedico.MascotaId)).FirstOrDefault();
            if (mascostaFound == null)
            {
                throw new Exception("No se encontro la mascota");
            }
            //** buscamos al veterinario
            var veterinarioFound = _veterinarios.Find(V => V.Matricula == servicioMedico.VeterinarioId).FirstOrDefault();
            if (veterinarioFound == null)
            {
                throw new Exception("No se encontro al veterinario");
            }
            //*creamos el servicio medico
            var newServicioMedico = new ServicioMedico
            {
                Id = Guid.NewGuid(),
                Costo = servicioMedico.Costo,
                Descripcion = servicioMedico.Descripcion,
                Fecha = servicioMedico.FechaServicio,
                MascotaId = mascostaFound.Id.ToString(),
                VeterinarioId = veterinarioFound.Matricula
            };

            //? Guardamos el servicio medico
            Insert(newServicioMedico);
            //? Creamos el registro clinico
            List<TiposDeServicio> tiposServicioList = new List<TiposDeServicio>();

            foreach (var servicios in servicioMedico.tiposServicio)
            {
                var tipoServicio = _tiposServicio.FindById(servicios);
                if (tipoServicio != null)
                {
                    tiposServicioList.Add(tipoServicio);
                }
            }

            _registroClinicoService.Insert(new RegistroClinico
            {
                Diagnostico = servicioMedico.Descripcion,
                Fecha = DateTime.Now,
                MascotaId = mascostaFound.Id,
                VeterinarioId = veterinarioFound.Id,
                Tratamiento = tiposServicioList,
            });

            //! creamos el arreglo con las especialiodades del veterinarios
            var especialidadesDto = veterinarioFound.especialidades?
               .Select(e => new EspecialidadReadDto
               {
                   Id = e.Id,
                   Nombre = e.nombre,
                   Descripcion = e.Descripcion
               }).ToList();


            //? creamos el DTo servicio medico que nos indica la informacion que debemos de devolver
            return new ServicioMedicoReadDto
            {
                Id = newServicioMedico.Id,
                Costo = newServicioMedico.Costo,
                Descripcion = newServicioMedico.Descripcion,
                Fecha = newServicioMedico.Fecha,
                Mascota = new MascotaReadDto
                {
                    Id = mascostaFound.Id,
                    Nombre = mascostaFound.Nombre ?? "",
                    Edad = mascostaFound.Edad,
                    Peso = mascostaFound.Peso,
                    Especie = new EspecieReadDto
                    {
                        Id = mascostaFound.Especie.Id,
                        NombreEspecie = mascostaFound.Especie.NombreEspecie ?? "",
                        Raza = mascostaFound.Especie.Raza ?? ""
                    }
                },
                Veterinario = new VeterinarioReadDto
                {
                    Matricula = veterinarioFound?.Matricula ?? "",
                    NombreCompleto = $"{veterinarioFound?.Nombre} {veterinarioFound?.Apellido}",
                    Especialidades = especialidadesDto
                }
            };

        }


        //?Metodos para obtener los servicios medicos
        public List<ServicioMedico> obtenerServicios()
        {
            var listServicios = GetAll();
            if (listServicios == null)
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
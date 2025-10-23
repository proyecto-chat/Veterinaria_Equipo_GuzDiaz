using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.VisualBasic;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.DB;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class VeterinarioService : ServicioGenerico<Veterinario>
    {
        private readonly ILiteCollection<Especialidades> _especialidades;
        private readonly ILiteCollection<Vacuna> _vacunas;
        private readonly MascotaService _mascotaService;
        public VeterinarioService(LiteDatabase db, MascotaService mascota) : base(db, "veterinarios")
        {
            _especialidades = db.GetCollection<Especialidades>("especialidades");
            _vacunas = db.GetCollection<Vacuna>("vacunas");
            _mascotaService = mascota;
        }


        public VeterinarioReadDto? register(VeterinarioCreateDto infoVeterinario)
        {
            if (infoVeterinario == null)
            {
                throw new Exception("La informacion esta vacia");
            }

            //! recorremos al arreglo y creamos la list de especialidades
            var especialidadesCreate = new List<Especialidades>();
            foreach (var espec in infoVeterinario.Especialidades)
            {
                var especialidad = _especialidades.Find(S => S.Id == Guid.Parse(espec)).FirstOrDefault();
                if (especialidad != null)
                {
                    especialidadesCreate.Add(especialidad);
                }

            }

            var veterinario = new Veterinario
            {
                Apellido = infoVeterinario.Apellido,
                Direccion = infoVeterinario.Direccion,
                DNI = infoVeterinario.DNI,
                Edad = infoVeterinario.Edad,
                especialidades = especialidadesCreate,
                Matricula = infoVeterinario.Matricula,
                Nombre = infoVeterinario.Nombre,
                servicioMedicos = new(),
                Telefono = infoVeterinario.Telefono,
            };

            //! hago uso de la clase abastracta para insertar el veterinario    
            Insert(veterinario);

            return new VeterinarioReadDto
            {
                Especialidades = especialidadesCreate.Select(e => new EspecialidadReadDto
                {
                    Id = e.Id,
                    Nombre = e.nombre
                }).ToList(),
                Matricula = infoVeterinario.Matricula,
                NombreCompleto = $"{infoVeterinario.Nombre}+{infoVeterinario.Apellido} "
            };

        }

        public List<Veterinario> obtenerVeterinarios()
        {
            var listVeterinarios = GetAll();
            if (listVeterinarios == null && !listVeterinarios.Any())
            {
                throw new Exception("no se encontaron veterinarios registrados");
            }
            return listVeterinarios;
        }

        public Veterinario obtenerVeterinarioMatricula(string matricula)
        {
            var resposne = GetOne(v => v.Matricula == matricula);
            if (resposne == null)
            {
                throw new Exception("El veterinario no existe");
            }
            return resposne;
        }

        // TODO: probar este metodo
        public List<Veterinario> obtenerVeterinarioConMasRegistrosClinicos(DateTime desdeFecha, DateTime hastaFecha)
        {
            var veterinarios = GetAll();

            var veterinariosConConteo = veterinarios.Select(v => new
            {
                Veterinario = v,
                ConteoServicios = v.servicioMedicos.Find(s => s.Fecha >= desdeFecha && s.Fecha <= hastaFecha)
            });

            var mejoresVeterinarios = veterinariosConConteo
                .OrderByDescending(vc => vc.ConteoServicios)
                .Select(vc => vc.Veterinario)
                .ToList();

            return mejoresVeterinarios;
        }

        public bool actualizarInfoVeterinario(VeterinarioUpdateDto newInfo, string matricula)
        {
            var veterinarioExist = GetOne(v => v.Matricula == matricula);

            if (veterinarioExist == null)
            {
                throw new Exception("El veterinario no existe");
            }

            if (!string.IsNullOrWhiteSpace(newInfo.Apellido))
            {
                veterinarioExist.Apellido = newInfo.Apellido;
            }

            if (!string.IsNullOrWhiteSpace(newInfo.Nombre))
            {
                veterinarioExist.Nombre = newInfo.Nombre;
            }

            if (!string.IsNullOrWhiteSpace(newInfo.Telefono))
            {
                veterinarioExist.Telefono = newInfo.Telefono;
            }

            if (!string.IsNullOrWhiteSpace(newInfo.Direccion))
            {
                veterinarioExist.Direccion = newInfo.Direccion;
            }
            if (newInfo.Edad > 0)
            {
                veterinarioExist.Edad = newInfo.Edad;
            }

            var response = Update(veterinarioExist);
            if (response == false)
            {
                throw new Exception("no se logro actualizar la informacion del veterinario");
            }
            return true;
        }

        //TODO: Modificar el objeto vacuna para que se adapte al DTO

        public RegistroVacunaReadDto asignarVacunas(List<string> vacunas, string idMascota, DateTime fechaAplicacion)
        {
            var mascota = _mascotaService.GetOne(m => m.Id == Guid.Parse(idMascota));

            if (mascota == null)
                throw new Exception("La mascota no existe");

            var vacunasAplicadas = new List<Vacuna>();

            foreach (var vacunaIdStr in vacunas)
            {
                var vacuna = _vacunas.FindOne(v => v.Id == Guid.Parse(vacunaIdStr));
                if (vacuna != null)
                {
                    var nuevaVacuna = new vacunasAplicadas
                    {
                        Id = vacuna.Id,
                        Nombre = vacuna.Nombre,
                        Descripcion = vacuna.Descripcion,
                        FechaAplicacion = fechaAplicacion
                    };

                    vacunasAplicadas.Add(nuevaVacuna);
                    mascota.Vacunas.Add(nuevaVacuna);
                }
            }

            _mascotaService.Update(mascota);

            return new RegistroVacunaReadDto
            {
                nombreMascota = mascota.Nombre,
                vacunas = vacunasAplicadas
            };
        }



        public bool eliminarVeterinario(string matricula)
        {

            var response = Delete(v => v.Matricula == matricula);
            if (!response)
            {
                throw new Exception("No se logro elimnar al veterinario");
            }
            return response;
        }


    }
}
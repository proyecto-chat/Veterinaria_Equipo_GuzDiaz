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
        private readonly ILiteCollection<Veterinario> _veterinario;
        private readonly MascotaService _mascotaService;
        public VeterinarioService(LiteDatabase db, MascotaService mascota) : base(db, "veterinarios")
        {
            _especialidades = db.GetCollection<Especialidades>("especialidades");
            _vacunas = db.GetCollection<Vacuna>("vacunas");
            _veterinario = db.GetCollection<Veterinario>("veterinarios");
            _mascotaService = mascota;
        }


        //* Metodo para registrar un veterinario */
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
                Console.WriteLine("id especi:" + espec);
                var especialidad = _especialidades.Find(S => S.Id == Guid.Parse(espec)).FirstOrDefault();
                if (especialidad != null)
                {
                    Console.WriteLine("Especialidad encontrad: " + especialidad.Descripcion);
                    especialidadesCreate.Add(new Especialidades
                    {
                        Id = especialidad.Id,
                        Descripcion = especialidad.Descripcion,
                        nombre = especialidad.nombre,
                    });
                }
            }

            string raw = Guid.NewGuid().ToString("N");
            string plate = raw.Substring(0, 6).ToUpper();

            //! creamos el objeto veterinario
            var veterinario = new Veterinario
            {
                Apellido = infoVeterinario.Apellido,
                Direccion = infoVeterinario.Direccion,
                DNI = infoVeterinario.DNI,
                Edad = infoVeterinario.Edad,
                especialidades = especialidadesCreate,
                Matricula = plate,
                Nombre = infoVeterinario.Nombre,
                servicioMedicos = new(),
                Telefono = infoVeterinario.Telefono,
            };

            //! hago uso de la clase abastracta para insertar el veterinario    
            Insert(veterinario);

            return new VeterinarioReadDto
            {
                Especialidades = veterinario.especialidades.Select(e => new EspecialidadReadDto
                {
                    Id = e.Id,
                    Nombre = e.nombre,
                    Descripcion = e.Descripcion
                }).ToList(),
                Matricula = plate,
                NombreCompleto = $"{infoVeterinario.Nombre}+{infoVeterinario.Apellido} "
            };

        }

        public List<Veterinario> obtenerVeterinarios()
        {
            var listVeterinarios = _veterinario.FindAll().ToList();
            if (listVeterinarios == null && !listVeterinarios.Any())
            {
                throw new Exception("no se encontaron veterinarios registrados");
            }

            foreach (var vet in listVeterinarios)
            {
                foreach (var esp in vet.especialidades)
                {
                    Console.WriteLine("especialidad: " + esp.nombre);
                }

            }

            return listVeterinarios;
        }

        public Veterinario obtenerVeterinarioporID(string id)
        {
            var resposne = GetOne(v => v.Id == Guid.Parse(id));
            if (resposne == null)
            {
                throw new Exception("El veterinario no existe");
            }
            return resposne;
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

        //* Metodo para obetener los veterinarios con mas registros clinicos en un rango de fechas
        public List<Veterinario> obtenerVeterinarioConMasRegistrosClinicos(DateTime desdeFecha, DateTime hastaFecha)
        {
            var veterinarios = GetAll();

            var veterinariosConConteo = veterinarios.Select(v => new
            {
                Veterinario = v,
                ConteoServicios = v.servicioMedicos.Find(s => s.Fecha >= desdeFecha && s.Fecha <= hastaFecha)
            });
            //* Ordenar por conteo descendente y seleccionar los veterinarios
            var mejoresVeterinarios = veterinariosConConteo
                .OrderByDescending(vc => vc.ConteoServicios)
                .Select(vc => vc.Veterinario)
                .ToList();

            return mejoresVeterinarios;
        }

        //* Metodo para actualizar la informacion del veterinario
        public bool actualizarInfoVeterinario(VeterinarioUpdateDto newInfo, string matricula)
        {
            var veterinarioExist = GetOne(v => v.Matricula == matricula);

            //* Validaciones
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

        //* Modificar el objeto vacuna para que se adapte al DTO

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
                    // ?Crear una nueva instancia de vacunasAplicadas con la fecha de aplicación
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


        //* Metodo para eliminar un veterinario
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
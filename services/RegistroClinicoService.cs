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
    public class RegistroClinicoService : ServicioGenerico<RegistroClinico>
    {
        private const string DB_FILE = "examen.db";
        private readonly ILiteCollection<TiposDeServicio> _tiposServicio;

        public RegistroClinicoService() : base("registroclinico")
        {
            var db = new LiteDatabase(DB_FILE);
            _tiposServicio = db.GetCollection<TiposDeServicio>("tiposdeservicio");
        }


        //** Agregamos un nuevo registro clinico
        public bool agregarRegistro(ServicioMedicoCreateDto registro, Veterinario veterinario, Mascota mascota)
        {
            if (registro == null || veterinario == null || mascota == null)
            {
                throw new Exception("La informacion no es correcta");
            }

            //** buscamos los tipos de servicio y creamos el registro clinico

            List<TiposDeServicio> tiposServicioList = new List<TiposDeServicio>();
            foreach (var servicios in registro.tiposServicio)
            {
                var tipoServicio = _tiposServicio.FindById(servicios);
                if (tipoServicio != null)
                {
                    tiposServicioList.Add(tipoServicio);
                }
            }

            Insert(new RegistroClinico
            {
                Diagnostico = registro.Descripcion,
                Fecha = DateTime.Now,
                Mascota = mascota,
                Veterinario = veterinario,
                Tratamiento = tiposServicioList,
            });
            return true;
        }

        //** Obtenemos el historial clinico de mascota por medio de su id
        public List<RegistroClinicoReadDto> obtenerHistorialClinicoPorMascota(Guid mascotaId)
        {
            var registros = GetList(R => R.Mascota.Id == mascotaId);

            //*transformamos los registros a DTO
            List<CitasReadDto> citasFinalizadas = registros.Select(r => new CitasReadDto
            {
                fecha = r.Fecha,
                diagnostico = r.Diagnostico,
                nombreVeterinario = $"{r.Veterinario.Nombre} {r.Veterinario.Apellido}",
                tratamiento = r.Tratamiento?.Select(t => new TiposDeServicio
                {
                    id = t.id,
                    NombreServicio = t.NombreServicio,
                    Descripcion = t.Descripcion,
                }).ToList() ?? new List<TiposDeServicio>(),
            }).ToList();

            List<VacunaReadDto> vacunasAplicadas = registros
                .FirstOrDefault()?.Mascota.Vacunas?
                .Select(v => new VacunaReadDto
                {
                    nombre = v.Nombre,
                    fechaAplicacion = v.FechaAplicacion,
                    descripcion = v.Descripcion,
                    estaVencida = v.EstaVencida(),
                }).ToList() ?? new List<VacunaReadDto>();

            return new List<RegistroClinicoReadDto>
            {
                new RegistroClinicoReadDto
                {
                    nombreMascota = registros.FirstOrDefault()?.Mascota.Nombre ?? string.Empty,
                    idMascota = mascotaId.ToString(),
                    citas = citasFinalizadas,
                    vacunas = vacunasAplicadas,
                }
            };


        }


    }

    //? Dtos para mostar la informacion de registro clinico

    public class RegistroClinicoReadDto
    {
        public string nombreMascota { get; set; }
        public string idMascota { get; set; }
        public List<CitasReadDto> citas { get; set; }
        public List<VacunaReadDto> vacunas { get; set; }
    }

    public class VacunaReadDto
    {
        public string nombre { get; set; }
        public DateTime fechaAplicacion { get; set; }
        public string descripcion { get; set; }
        public bool estaVencida { get; set; }
    }

    public class CitasReadDto
    {
        public DateTime fecha { get; set; }
        public string diagnostico { get; set; }
        public List<TiposDeServicio> tratamiento { get; set; }
        public string nombreVeterinario { get; set; }
    }


}
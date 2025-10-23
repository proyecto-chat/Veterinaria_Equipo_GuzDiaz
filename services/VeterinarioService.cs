using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.VisualBasic;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class VeterinarioService: ServicioGenerico<Veterinario>
    {
        public VeterinarioService(): base("veterinarios")
        {
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
                especialidadesCreate.Add(new Especialidades
                {
                    Id = Guid.NewGuid(),
                    nombre = espec.nombre,
                    Descripcion = espec.descripcion,
                });
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
                Especialidades = new List<EspecialidadReadDto>(),
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
        public List<Veterinario> ObtenerMejoresVeterinarios(DateTime desdeFecha, DateTime hastaFecha)
        {
            var veterinarios = GetAll();

            var veterinariosConConteo = veterinarios.Select(v => new
            {
                Veterinario = v,
                ConteoServicios = v.servicioMedicos.Count(s => s.Fecha >= desdeFecha && s.Fecha <= hastaFecha)
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

        //TODO: MModificar el objeto vacuna para que se adapte al DTO

        public RegistroVacunaReadDto asignarVacunas(List<string> vacunas, string idMascota)
        {
            return null;
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
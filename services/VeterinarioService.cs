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
    /*
        *registrar mascotas u dueños --listo
        TODO: obtener historial clinico
        TODO: obtener mascota con sus dueños
        TODO: actualizar informacion del dueño
    
    */
    public class VeterinarioService
    {
        private const string DB_FILE = "examen.db";
        private readonly ILiteCollection<Veterinario> _veterinarios;

        public VeterinarioService()
        {
            var db = new LiteDatabase(DB_FILE);
            _veterinarios = db.GetCollection<Veterinario>("veterinarios");
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

            _veterinarios.Insert(veterinario);

            return new VeterinarioReadDto
            {
                Especialidades = new List<EspecialidadReadDto>(),
                Matricula = infoVeterinario.Matricula,
                NombreCompleto = $"{infoVeterinario.Nombre}+{infoVeterinario.Apellido} "
            };

        }

        public List<Veterinario> obtenerVeterinarios()
        {
            var listVeterinarios = _veterinarios.FindAll().ToList();
            if (listVeterinarios == null && !listVeterinarios.Any())
            {
                throw new Exception("no se encontaron veterinarios registrados");
            }
            return listVeterinarios;
        }

        public Veterinario obtenerVeterinarioMatricula(string matricula)
        {
            var resposne = _veterinarios.FindOne(v => v.Matricula == matricula);
            if (resposne == null)
            {
                throw new Exception("El veterinario no existe");
            }
            return resposne;
        }

        public bool actualizarInfoVeterinario(VeterinarioUpdateDto newInfo)
        {
            var veterinarioExist = _veterinarios.FindOne(v => v.Matricula == newInfo.Matricula);

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
      
            
            var response = _veterinarios.Update(veterinarioExist);
            if (response == false)
            {
                throw new Exception("no se logro actualizar la informacion del veterinario");
            }
            return true;
        }

        public bool eliminarVeterinario(string matricula)
        {

            var veterinarioFound = _veterinarios.FindOne(v => v.Matricula == matricula);
            if (veterinarioFound == null)
            {
                throw new Exception("El veterinario no existe");
            }
            var response = _veterinarios.Delete(veterinarioFound.Id);
            if (response == false)
            {
                throw new Exception("No se logro elimnar al veterinario");
            }
            return response;
        }


    }
}
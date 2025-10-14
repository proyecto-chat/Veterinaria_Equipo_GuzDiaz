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
        private readonly ILiteCollection<Dueño> _dueños;
        private readonly ILiteCollection<Mascota> _mascotas;
        private readonly ILiteCollection<Especialidades> _especialidades;
        private readonly ILiteCollection<Veterinario> _veterinarios;

        public VeterinarioService()
        {
            var db = new LiteDatabase(DB_FILE);
            _dueños = db.GetCollection<Dueño>("dueños");
            _mascotas = db.GetCollection<Mascota>("mascotas");
            _especialidades = db.GetCollection<Especialidades>("especialidades");
            _veterinarios = db.GetCollection<Veterinario>("veterinarios");
        }

        public VeterinarioReadDto? register(VeterinarioCreateDto infoVeterinario)
        {
            if (infoVeterinario == null)
            {
                throw new Exception("La informacion esta vacia");
            }

            var veterinario = new Veterinario
            {
                Apellido = infoVeterinario.Apellido,
                Direccion = infoVeterinario.Direccion,
                DNI = infoVeterinario.DNI,
                Edad = infoVeterinario.Edad,
                especialidades = infoVeterinario.Especialidades ?? new List<Especialidades>(),
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


    }
}
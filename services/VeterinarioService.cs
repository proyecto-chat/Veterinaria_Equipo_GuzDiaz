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

        public VeterinarioService()
        {
            var db = new LiteDatabase(DB_FILE);
            _dueños = db.GetCollection<Dueño>("dueños");
            _mascotas = db.GetCollection<Mascota>("mascotas");
            _especialidades = db.GetCollection<Especialidades>("especialidades");
        }

        public void RegistrarMascota(RequestRegisterPet infoPet)
        {
            if (infoPet == null)
                throw new Exception("La información está vacía");

            var mascota = new Mascota
            {
                Nombre = infoPet.infoMascota.Nombre,
                Edad = infoPet.infoMascota.Edad,
                Peso = infoPet.infoMascota.Peso,
                Especie = new Especie
                {
                    NombreEpecie = infoPet.infoMascota.Especie.NombreEpecie,
                    Raza = infoPet.infoMascota.Especie.Raza
                },
                registroClinicos = new()
            };

            _mascotas.Insert(mascota);

            var dueño = new Dueño
            {
                Nombre = infoPet.infoDueño.Nombre,
                Apellido = infoPet.infoDueño.Apellido,
                Edad = infoPet.infoDueño.Edad,
                Direccion = infoPet.infoDueño.Direccion,
                Telefono = infoPet.infoDueño.Telefono,
                DNI = infoPet.infoDueño.DNI,
                Mascotas = new()
            };

            mascota.dueño = dueño;
            dueño.Mascotas.Add(mascota);

            _dueños.Insert(dueño);
            _mascotas.Update(mascota); // actualizamos la referencia del dueño

            Console.WriteLine($"Registrado {dueño.Nombre} con su mascota {mascota.Nombre}");
        }

        public List<RegistroClinico> ObtenerHistorialClinico(RequestRegisterPet infoPet)
        {
            if (infoPet.infoMascota == null)
            {
                throw new Exception("La informacion de la mascota está vacía");
            }
            return infoPet.infoMascota.registroClinicos;
        }

        public List<Mascota> ObtenerMascotasDelDueño(RequestRegisterPet infoPet)
        {
            if (infoPet.infoDueño == null)
            {
                throw new Exception("La información del dueño está vacía");
            }

            return infoPet.infoDueño.Mascotas;
        }

        public void ActualizarInfoDueño(Dueño dueño)
        {
            if (dueño == null)
            {
                throw new Exception("La información del dueño está vacía");
            }
            _dueños.Update(dueño);
        }
    }
}
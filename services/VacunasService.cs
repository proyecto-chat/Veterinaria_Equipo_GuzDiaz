using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class VacunasService : ServicioGenerico<Vacuna>
    {
        public VacunasService(LiteDatabase db) : base(db,"vacunas")
        {

        }

        public Vacuna Register(VacunaCreateDto vacuna)
        {
            if (vacuna == null)
            {
                throw new Exception("La informacion no es correcta");
            }
            var newVacuna = new Vacuna
            {
                Id = Guid.NewGuid(),
                Nombre = vacuna.Nombre,
                Descripcion = vacuna.Descripcion,
            };

            Insert(newVacuna);
            return newVacuna;
        }

        public List<Vacuna> GetAllVaccines()
        {
            var vacunas = GetAll();
            if (vacunas == null || !vacunas.Any())
            {
                throw new Exception("No hay vacunas registradas");
            }
            return vacunas;
        }        

    }
}
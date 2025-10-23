using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class EspecialidadesService: ServicioGenerico<Especialidades>
    {
        public EspecialidadesService(LiteDatabase db) : base(db,"especialidades")
        {

        }

        public Especialidades crearEspecialidad(string nombre, string descripcion)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion))
            {
                throw new Exception("La informacion no es correcta");
            }

            var nuevaEspecialidad = new Especialidades
            {
                nombre = nombre,
                Descripcion = descripcion
            };

            Insert(nuevaEspecialidad);
            return nuevaEspecialidad;
        }
        
        public List<Especialidades> obtenerEspecialidades()
        {
            return GetAll().ToList();
        }


    }
}
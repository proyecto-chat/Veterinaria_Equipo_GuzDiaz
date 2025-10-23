using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class TiposServicioService : ServicioGenerico<TiposDeServicio>
    {
        public TiposServicioService(LiteDatabase db) : base(db,"tiposdeservicio")
        {

        }

        public TiposDeServicio crearTipoServicio(string nombreServicio, string descripcion)
        {
            if (string.IsNullOrEmpty(nombreServicio) || string.IsNullOrEmpty(descripcion))
            {
                throw new Exception("La informacion no es correcta");
            }

            var nuevoTipoServicio = new TiposDeServicio
            {
                NombreServicio = nombreServicio,
                Descripcion = descripcion
            };

            Insert(nuevoTipoServicio);
            return nuevoTipoServicio;
        }
        
        public List<TiposDeServicio> obtenerTiposServicio()
        {
            return GetAll().ToList();
        }


    }
}
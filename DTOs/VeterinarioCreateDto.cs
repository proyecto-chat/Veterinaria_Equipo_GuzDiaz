using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class VeterinarioCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        [BsonRef("especialidades")]
        public List<EspecialidadesDTos> Especialidades { get; set; } = new();
    }

    public class EspecialidadesDTos
    {
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
    }

}
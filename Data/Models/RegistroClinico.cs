using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.Data.Models
{
    public class RegistroClinico
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string Diagnostico { get; set; } = string.Empty;
    public List<TiposDeServicio> Tratamiento { get; set; } = new();

    // Guardamos solo los IDs
    public Guid VeterinarioId { get; set; }
    public Guid MascotaId { get; set; }

    // Propiedades de navegación opcionales (solo para mapear a DTO)
    [BsonIgnore] 
    public Veterinario? Veterinario { get; set; }
    [BsonIgnore] 
    public Mascota? Mascota { get; set; }
}

}
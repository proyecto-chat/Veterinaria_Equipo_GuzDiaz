using LiteDB;
using Veterinaria.Data.Models;

public class ServicioMedico
{
    public Guid Id { get; set; }
    public float Costo { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }

    public string MascotaId { get; set; }   // Solo el Id
    public string VeterinarioId { get; set; } // Solo el Id

    [BsonIgnore] // Ignora propiedades que no deben serializarse
    public Mascota Mascota { get; set; }

    [BsonIgnore]
    public Veterinario Veterinario { get; set; }
}



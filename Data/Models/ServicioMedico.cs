using LiteDB;
using Veterinaria.Data.Models;

public class ServicioMedico
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Descripcion { get; set; } = string.Empty;
    public string Detalles { get; set; } = string.Empty;

    // Relaciones
    public Veterinario Veterinario { get; set; }
    public Mascota Mascota { get; set; }

    public double Costo { get; set; }
    public DateTime? FechaFinalizacion { get; set; }
}


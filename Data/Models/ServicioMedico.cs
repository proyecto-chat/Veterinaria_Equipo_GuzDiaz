using LiteDB;
using Veterinaria.Data.Models;

public class ServicioMedico
{
    [BsonId]
    public int IdService { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Descripcion { get; set; }
    public string Detalles { get; set; }

    // Relaciones
    public Veterinario Veterinario { get; set; }
    public Mascota Mascota { get; set; }

    public DateTime? FechaFinalizacion { get; set; }
}

public class RequestCrearServicio
{

    public string Descripcion { get; set; }
    public string Detalles { get; set; }

    // Relaciones
    public Veterinario Veterinario { get; set; }
    public Mascota Mascota { get; set; }
}

public class RequestCrearHistorialClinico
{
    public int idSerivcioMedico { get; set; }
    public string Diagnostico { get; set; }
    public string Tratamiento { get; set; }
}

public class RequestFinalizarServico
{
    public int id { get; set; }
}

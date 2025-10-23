using Veterinaria_Equipo_GuzDiaz.DTOs;

public class DueñoConMascotaReadDto
{
    public Guid Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public int CantidadMascotas { get; set; }
    public List<MascotaReadDto> Mascotas { get; set; } = new();
}
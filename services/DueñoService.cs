using LiteDB;
using Microsoft.AspNetCore.Http.HttpResults;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services;

public class DueñoService : ServicioGenerico<Dueño>
{
    private const string DB_FILE = "examen.db";
    private readonly ILiteCollection<Mascota> _mascotas;

    public DueñoService() : base("dueños")
    {
        var db = new LiteDatabase(DB_FILE);
        _mascotas = db.GetCollection<Mascota>("mascotas");
    }

    private DueñoReadDto MapToReadDueño(Dueño dueño)
    {
        return new DueñoReadDto
        {
            Id = dueño.Id,
            NombreCompleto = $"{dueño.Nombre} {dueño.Apellido}",
            DNI = dueño.DNI,
            Mascotas = dueño.Mascotas?.Select(m => new MascotaReadDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Edad = m.Edad,
                Peso = m.Peso,
                Especie = new EspecieReadDto
                {
                    Id = m.Especie?.Id ?? Guid.Empty,
                    NombreEspecie = m.Especie?.NombreEspecie ?? "",
                    Raza = m.Especie?.Raza ?? ""
                }
            }).ToList() ?? new List<MascotaReadDto>()
        };
    }

    public DueñoReadDto? registrarNuevoDueño(DueñoCreateDto infoDueño)
    {
        if (infoDueño == null) return null;
        var dueñoExiste = GetOne(d => d.DNI == infoDueño.DNI);
        if (dueñoExiste != null) throw new Exception("El dueño ya esta registrado");
        var dueño = new Dueño
        {
            Id = Guid.NewGuid(),
            Nombre = infoDueño.Nombre,
            Apellido = infoDueño.Apellido,
            Edad = infoDueño.Edad,
            Telefono = infoDueño.Telefono,
            DNI = infoDueño.DNI,
            Direccion = infoDueño.Direccion,
            Mascotas = new List<Mascota>()
        };
        Insert(dueño);
        var resultado = MapToReadDueño(dueño);
        return resultado;
    }

    public List<DueñoReadDto> ObtenerTodosDueños()
    {
        var dueños = GetAll();
        if (!dueños.Any()) throw new Exception("No hay dueños registrados");
        return dueños.Select(MapToReadDueño).ToList();
    }

    public DueñoReadDto? obtenerDueño(string dni)
    {
        if (dni == null) throw new Exception("DNI invalido");
        var dueño = GetOne(d => d.DNI == dni);
        if (dueño == null) return null;
        var resultado = MapToReadDueño(dueño);
        return resultado;
    }

    public bool actualizarDueño(string dni, DueñoUpdateDto dueñoUp)
    {
        if (dni == null) throw new Exception("DNI invalido");
        var dueño = GetOne(d => d.DNI == dni);
        if (dueño == null) return false;
        dueño.Nombre = dueñoUp.Nombre ?? dueño.Nombre;
        dueño.Apellido = dueñoUp.Apellido ?? dueño.Apellido;
        dueño.Edad = dueñoUp.Edad;
        dueño.Telefono = dueñoUp.Telefono ?? dueño.Telefono;
        dueño.DNI = dueñoUp.DNI ?? dueño.DNI;
        dueño.Direccion = dueñoUp.Direccion ?? dueño.Direccion;
        return Update(dueño);
    }

    public bool eliminarDueño(string dni)
    {
        if (dni == null) throw new Exception("DNI invalido");
        return Delete(d => d.DNI == dni);
    }

    public List<MascotaReadDto>? obtenerMascotasDueño(string dni)
    {
        if (dni == null) throw new Exception("DNI invalido");
        var mascotasDueño = _mascotas.Find(m => m.dueñoDni == dni).ToList();
        if (mascotasDueño == null || mascotasDueño.Count == 0)
        {
            return new List<MascotaReadDto>();
        }
        return mascotasDueño.Select(m => new MascotaReadDto
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Edad = m.Edad,
            Peso = m.Peso,
            Especie = new EspecieReadDto
            {
                Id = m.Especie?.Id ?? Guid.Empty,
                NombreEspecie = m.Especie?.NombreEspecie ?? "",
                Raza = m.Especie?.Raza ?? ""
            }
        }).ToList();
    }

    public List<DueñoConMascotaReadDto>? obtenerDueñosConNMascotas(int cantMascotas)
    {
        if (cantMascotas <= 0) throw new Exception("La cantidad de mascotas no puede ser negativa o igual a 0");
        var dueños = GetAll();
        if (dueños == null || !dueños.Any()) throw new Exception("No hay dueños registrados");
        var response = dueños
            .Where(d => d.Mascotas.Count >= cantMascotas)
            .Select(d => new DueñoConMascotaReadDto
            {
                Id = d.Id,
                NombreCompleto = $"{d.Nombre} {d.Apellido}",
                DNI = d.DNI,
                CantidadMascotas = d.Mascotas?.Count ?? 0,
                Mascotas = d.Mascotas?.Select(m => new MascotaReadDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Edad = m.Edad,
                    Peso = m.Peso,
                    Especie = new EspecieReadDto
                    {
                        Id = m.Especie?.Id ?? Guid.Empty,
                        NombreEspecie = m.Especie?.NombreEspecie ?? "",
                        Raza = m.Especie?.Raza ?? ""
                    }
                }).ToList() ?? new List<MascotaReadDto>()
            }).ToList();
        if (!response.Any()) throw new Exception($"No hay dueños con {cantMascotas} o más mascotas");
        return response;
    }
}
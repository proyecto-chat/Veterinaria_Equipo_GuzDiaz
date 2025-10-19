using LiteDB;
using Microsoft.AspNetCore.Http.HttpResults;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services;

public class DueñoService
{
    private const string DB_FILE = "examen.db";
    private readonly ILiteCollection<Dueño> _dueños;
    private readonly ILiteCollection<Mascota> _mascotas;
    private readonly ILiteCollection<Especie> _especies;

    public DueñoService()
    {
        var db = new LiteDatabase(DB_FILE);
        _dueños = db.GetCollection<Dueño>("dueños");
        _mascotas = db.GetCollection<Mascota>("mascotas");
        _especies = db.GetCollection<Especie>("especies");
    }

    private DueñoReadDto MapToReadDueño(Dueño dueño)
    {
        return new DueñoReadDto
        {
            Id = dueño.Id,
            NombreCompleto = $"{dueño.Nombre} {dueño.Apellido}",
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
        if (infoDueño == null)
        {
            return null;
        }
        var dueño = new Dueño
        {
            Id = Guid.NewGuid(),
            Nombre = infoDueño.Nombre,
            Apellido = infoDueño.Apellido,
            Edad = infoDueño.Edad,
            Telefono = infoDueño.Telefono,
            DNI = infoDueño.DNI,
            Direccion = infoDueño.Direccion,
            Mascotas = infoDueño.Mascotas?.Select(m => new Mascota
            {
                Id = Guid.NewGuid(),
                Nombre = m.Nombre,
                Edad = m.Edad,
                Peso = m.Peso,
                Especie = new Especie
                {
                    Id = Guid.NewGuid(),
                    NombreEspecie = m.Especie?.NombreEspecie ?? "",
                    Raza = m.Especie?.Raza ?? ""
                },
            }).ToList() ?? new List<Mascota>()
        };
        _dueños.Insert(dueño);
        var dueñoGuardado = _dueños.FindById(dueño.Id);
        var resultado = MapToReadDueño(dueñoGuardado);
        return resultado;
    }

    public List<DueñoReadDto> ObtenerTodosDueños()
    {
        var dueños = _dueños.FindAll().ToList();
        return dueños.Select(d => new DueñoReadDto
        {
            Id = d.Id,
            NombreCompleto = $"{d.Nombre} {d.Apellido}",
            Mascotas = d.Mascotas?.Select(m => new MascotaReadDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Edad = m.Edad,
                Peso = m.Peso,
                Especie = new EspecieReadDto
                {
                    NombreEspecie = m.Especie?.NombreEspecie ?? "",
                    Raza = m.Especie?.Raza ?? ""
                }
            }).ToList() ?? new List<MascotaReadDto>()
        }).ToList();
    }

    public DueñoReadDto? obtenerDueño(string id)
    {
        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return null;
        }
        var dueño = _dueños.FindById(guidId);
        var resultado = MapToReadDueño(dueño);
        return resultado;
    }

    public bool actualizarDueño(string id, DueñoUpdateDto dueñoUp)
    {

        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return false;
        }
        var dueño = _dueños.FindById(guidId);
        if (dueño == null)
        {
            return false;
        }
        dueño.Nombre = dueñoUp.Nombre ?? dueño.Nombre;
        dueño.Apellido = dueñoUp.Apellido ?? dueño.Apellido;
        dueño.Edad = dueñoUp.Edad;
        dueño.Telefono = dueñoUp.Telefono ?? dueño.Telefono;
        dueño.DNI = dueñoUp.DNI ?? dueño.DNI;
        dueño.Direccion = dueñoUp.Direccion ?? dueño.Direccion;
        return _dueños.Update(dueño);
    }

    public bool eliminarDueño(string id)
    {
        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return false;
        }
        return _dueños.Delete(guidId);
    }

    public List<MascotaReadDto>? obtenerMascotasDueño(string id)
    {
        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return null;
        }
        var dueño = _dueños.FindById(guidId);
        if (dueño == null)
        {
            return null;
        }
        var mascota = dueño.Mascotas.Select(m => new MascotaReadDto
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Edad = m.Edad,
            Peso = m.Peso,
            Especie = new EspecieReadDto
            {
                Id = m.Especie.Id,
                NombreEspecie = m.Especie.NombreEspecie ?? "",
                Raza = m.Especie.Raza ?? ""
            }
        }).ToList();
        return mascota;
    }
}
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services;

public class DueñoService
{
    private const string DB_FILE = "examen.db";
    private readonly ILiteCollection<Dueño> _dueños;

    public DueñoService()
    {
        var db = new LiteDatabase(DB_FILE);
        _dueños = db.GetCollection<Dueño>("dueños");
    }

    public DueñoReadDto? registrarNuevoDueño(DueñoCreateDto infoDueño)
    {
        if (infoDueño == null)
        {
            throw new Exception("La informacion del dueño está vacía");
        }
        var dueñoExiste = _dueños.FindOne(d => d.DNI == infoDueño.DNI);
        if (dueñoExiste != null)
        {
            throw new Exception("El dueño ya existe");
        }
        var dueño = new Dueño
        {
            Nombre = infoDueño.Nombre,
            Apellido = infoDueño.Apellido,
            Edad = infoDueño.Edad,
            Telefono = infoDueño.Telefono,
            DNI = infoDueño.DNI,
            Direccion = infoDueño.Direccion,
            Mascotas = infoDueño.Mascotas ?? new List<Mascota>()
        };
        _dueños.Insert(dueño);
        return new DueñoReadDto
        {
            Id = dueño.Id,
            Mascotas = new List<MascotaReadDto>(),
            NombreCompleto = $"{infoDueño.Nombre} {infoDueño.Apellido}"
        };
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
        if (id == null)
        {
            return null;
        }
        if (!Guid.TryParse(id, out Guid guidId))
        {
            return null;
        }
        var dueño = _dueños.FindById(guidId);
        if (dueño == null)
        {
            return null;
        }
        return new DueñoReadDto
        {
            Id = guidId,
            NombreCompleto = $"{dueño.Nombre} {dueño.Apellido}",
            Mascotas = dueño.Mascotas.Select(m => new MascotaReadDto
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
        };
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

    
}
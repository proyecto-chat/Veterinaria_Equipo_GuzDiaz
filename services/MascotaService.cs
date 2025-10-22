using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services;

public class MascotaService
{
    private const string DB_FILE = "examen.db";
    private readonly ILiteCollection<Mascota> _mascotas;
    private readonly ILiteCollection<Dueño> _dueños;

    public MascotaService()
    {
        var db = new LiteDatabase(DB_FILE);
        _mascotas = db.GetCollection<Mascota>("mascotas");
        _dueños = db.GetCollection<Dueño>("dueños");
    }

    private MascotaReadDto MapToReadMascota(Mascota mascota)
    {
        return new MascotaReadDto
        {
            Id = mascota.Id,
            Nombre = mascota.Nombre,
            Edad = mascota.Edad,
            Peso = mascota.Peso,
            Especie = new EspecieReadDto
            {
                Id = mascota.Especie?.Id ?? Guid.Empty,
                NombreEspecie = mascota.Especie?.NombreEspecie ?? "",
                Raza = mascota.Especie?.Raza ?? ""
            }
        };
    }
    
    public MascotaReadDto? registrarNuevaMascota(string dni, MascotaCreateDto infoMascota)
    {
        var dueño = _dueños.FindOne(d => d.DNI.Trim().ToLower() == dni.Trim().ToLower());
        if (infoMascota == null || dueño == null)
        {
            return null;
        }
        var mascota = new Mascota
        {
            Id = Guid.NewGuid(),
            Nombre = infoMascota.Nombre,
            Edad = infoMascota.Edad,
            Peso = infoMascota.Peso,
            Especie = new Especie
            {
                Id = Guid.NewGuid(),
                NombreEspecie = infoMascota.Especie?.NombreEspecie ?? "",
                Raza = infoMascota.Especie?.Raza ?? ""
            },
            registroClinicos = new List<RegistroClinico>(),
            dueñoDni = dueño.DNI
        };
        _mascotas.Insert(mascota);
        if (dueño.Mascotas == null)
        {
            dueño.Mascotas = new List<Mascota>();
        }
        dueño.Mascotas.Add(mascota);
        _dueños.Update(dueño);
        var mascotaEncontrada = _mascotas.FindById(mascota.Id);
        var resultado = MapToReadMascota(mascotaEncontrada);
        return resultado;
    }


    public MascotaReadDto? obtenerMascota(string id)
    {
        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return null;
        }
        var mascota = _mascotas.FindById(guidId);
        var resultado = MapToReadMascota(mascota);
        return resultado;
    }

    public List<MascotaReadDto> obtenerTodasMascotas()
    {
        var mascotas = _mascotas.FindAll().ToList();
        return mascotas.Select(m => new MascotaReadDto
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
        }).ToList() ?? new List<MascotaReadDto>();
    }

    public bool actualizarMascota(string id, MascotaUpdateDto mascotaUp)
    {
        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return false;
        }
        var mascota = _mascotas.FindById(guidId);
        mascota.Nombre = mascotaUp.Nombre ?? "";
        mascota.Edad = mascotaUp.Edad;
        mascota.Peso = mascotaUp.Peso;
        return _mascotas.Update(mascota);
    }

    public bool eliminarMascota(string id)
    {
        if (id == null || !Guid.TryParse(id, out Guid guidId))
        {
            return false;
        }
        return _mascotas.Delete(guidId);
    }
}
using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services;

public class MascotaService : ServicioGenerico<Mascota>
{
    private const string DB_FILE = "examen.db";
    private readonly ILiteCollection<Dueño> _dueños;

    public MascotaService() : base("mascotas")
    {
        //var db = new LiteDatabase(DB_FILE);
        _dueños = _database.GetCollection<Dueño>("dueños");
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
        if (infoMascota == null) return null;
        var dueño = _dueños.FindOne(d => d.DNI.Trim().ToLower() == dni.Trim().ToLower());
        if (dueño == null) throw new Exception("El dueño no esta registrado");
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
        Insert(mascota);
        if (dueño.Mascotas == null)
        {
            dueño.Mascotas = new List<Mascota>();
        }
        dueño.Mascotas.Add(mascota);
        _dueños.Update(dueño);
        var resultado = MapToReadMascota(mascota);
        return resultado;
    }

    public MascotaReadDto? obtenerMascota(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("El id no puede estar vacío");
        if (!Guid.TryParse(id, out Guid guidId)) throw new Exception("Id con formato incorrecto");
        var mascota = GetOne(m => m.Id == guidId);
        if (mascota == null) return null;
        var resultado = MapToReadMascota(mascota);
        return resultado;
    }

    public List<MascotaReadDto> obtenerTodasMascotas()
    {
        var mascotas = GetAll();
        if (!mascotas.Any()) throw new Exception("No hay mascotas registradas");
        return mascotas.Select(MapToReadMascota).ToList();
    }

    public bool actualizarMascota(string id, MascotaUpdateDto mascotaUp)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("El id no puede estar vacío");
        if (!Guid.TryParse(id, out Guid guidId)) throw new Exception("Id con formato incorrecto");
        var mascota = GetOne(m => m.Id == guidId);
        if (mascota == null) return false;
        mascota.Nombre = mascotaUp.Nombre ?? mascota.Nombre;
        mascota.Edad = mascotaUp.Edad;
        mascota.Peso = mascotaUp.Peso;
        return Update(mascota);
    }

    public bool eliminarMascota(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("El id no puede estar vacío");
        if (!Guid.TryParse(id, out Guid guidId)) throw new Exception("Id con formato incorrecto");
        return Delete(m => m.Id == guidId);
    }

    public List<MascotaReadDto> obtenerMascotasPorEspecie(string especie)
    {
        if (especie == null) throw new Exception("Especie invalida");
        var mascotas = GetAll();
        if (mascotas == null || !mascotas.Any()) throw new Exception("No hay mascotas registradas");
        var response = mascotas
            .Where(m => m.Especie.NombreEspecie.ToLower().Contains(especie.ToLower()))
            .Select(MapToReadMascota).ToList();
        if (!response.Any()) throw new Exception($"No se encontraron mascotas de especie {especie}");
        return response;
    }

    public List<MascotaReadDto> obtenerMascotasPorEdades(int edadInicial, int edadFinal)
    {
        if (edadInicial <= 0 || edadFinal <= 0) throw new Exception("Rango de edades invalida");
        var mascotas = GetAll();
        if (mascotas == null || !mascotas.Any()) throw new Exception("No hay mascotas registradas");
        var response = mascotas
            .Where(m => m.Edad >= edadInicial && m.Edad <= edadFinal)
            .Select(MapToReadMascota).ToList();
        if (!response.Any()) throw new Exception($"No se encontraron mascotas en el rango de {edadInicial} a {edadFinal}");
        return response;
    }
}
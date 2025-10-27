using LiteDB;
using Veterinaria.Data.Models;
using Veterinaria_Equipo_GuzDiaz.Data.DB;
using Veterinaria_Equipo_GuzDiaz.Data.Models;
using Veterinaria_Equipo_GuzDiaz.DTOs;

namespace Veterinaria_Equipo_GuzDiaz.services;

public class MascotaService : ServicioGenerico<Mascota>
{
    private readonly ILiteCollection<Dueño> _dueños;

    public MascotaService(LiteDatabase db) : base(db, "mascotas")
    {
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
            },
            Vacunas = mascota.Vacunas?.Select(v => new VacunaReadDto
            {
                nombre = v.Nombre,
                fechaAplicacion = v.FechaAplicacion,
                descripcion = v.Descripcion,
                estaVencida = v.EstaVencida()
            }).ToList() ?? new List<VacunaReadDto>()

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
            registroClinicos = new List<Guid>(),
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

    public List<MascotaReadDto> obtenerMascotasPorEdades(int edadInicial, int edadFinal, string especie)
    {
        if (edadInicial < 0 || edadFinal < 0)
            throw new ArgumentException("La edad no puede ser negativa");

        if (edadInicial > edadFinal)
            throw new ArgumentException("El rango de edades es inválido");

        var mascotas = GetAll() ?? new List<Mascota>();

        var response = mascotas
            .Where(m =>
                m.Edad >= edadInicial &&
                m.Edad <= edadFinal &&
                m.Especie?.NombreEspecie?.Equals(especie, StringComparison.OrdinalIgnoreCase) == true
            )
            .Select(MapToReadMascota)
            .ToList();

        return response;
    }


    public List<MascotaReadDto> obtenerMascotasConVacunasVencidas()
    {
        var mascotas = GetAll();
        if (mascotas == null || !mascotas.Any())
            throw new Exception("No hay mascotas registradas");

        var resultado = mascotas
            .Where(m => m.Vacunas != null && m.Vacunas.Any(v => v.FechaAplicacion.AddYears(1) <= DateTime.Now))
            .Select(MapToReadMascota)
            .ToList();

        if (!resultado.Any())
            throw new Exception("No se encontraron mascotas con vacunas vencidas");

        return resultado;
    }

}
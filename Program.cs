using LiteDB;
using Veterinaria_Equipo_GuzDiaz.Data.DB.Veterinaria_Equipo_GuzDiaz.Data.DB;
using Veterinaria_Equipo_GuzDiaz.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

/// ** configuramos la base de datos para que se llame una sola vez
builder.Services.AddSingleton<LiteDatabase>(_ => Database.Instance);


builder.Services.AddSingleton<VeterinarioService>();
builder.Services.AddSingleton<DueñoService>();
builder.Services.AddSingleton<ServicioService>();
builder.Services.AddSingleton<MascotaService>();
builder.Services.AddSingleton<RegistroClinicoService>();
builder.Services.AddSingleton<TiposServicioService>();
builder.Services.AddSingleton<EspecialidadesService>();
builder.Services.AddSingleton<VacunasService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

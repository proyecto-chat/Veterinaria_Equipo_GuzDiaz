using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Data.Models;

namespace Veterinaria_Equipo_GuzDiaz.DTOs
{
    public class RequestRegisterPet
    {
        public Dueño infoDueño { get; set; }
        public Mascota infoMascota { get; set; }
    }
}
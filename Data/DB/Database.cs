using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria_Equipo_GuzDiaz.Data.DB
{
    using LiteDB;

namespace Veterinaria_Equipo_GuzDiaz.Data.DB
{
    public static class Database
    {
        private static readonly Lazy<LiteDatabase> _instance = new Lazy<LiteDatabase>(() =>
        {
            return new LiteDatabase("examen.db");
        });

        public static LiteDatabase Instance => _instance.Value;
    }
}

}
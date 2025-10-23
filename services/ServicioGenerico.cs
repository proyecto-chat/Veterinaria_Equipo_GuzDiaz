using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiteDB;

namespace Veterinaria_Equipo_GuzDiaz.services
{
    public class ServicioGenerico<T> where T : class
    {
        private readonly ILiteCollection<T> _collection;
        protected readonly LiteDatabase _database;
        private const string DB_FILE = "examen.db";

        public ServicioGenerico(string collectionName)
        {
            _database = new LiteDatabase(DB_FILE);
            _collection = _database.GetCollection<T>(collectionName);
        }

        public List<T> GetAll() => _collection.FindAll().ToList();
        public List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate).ToList();
        }


        public T? GetOne(Expression<Func<T, bool>> predicate) => _collection.FindOne(predicate);

        public T Insert(T entity)
        {
            _collection.Insert(entity);
            return entity;
        }

        public bool Update(T entity) => _collection.Update(entity);

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            var entity = _collection.FindOne(predicate);
            if (entity == null) return false;

            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null) return false;

            var id = (Guid)idProp.GetValue(entity);
            return _collection.Delete(id);
        }
    }
}
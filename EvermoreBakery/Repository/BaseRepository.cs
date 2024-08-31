using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvermoreBakery.Service
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public DbSet<T> repo;

        public BaseRepository()
        {
            _context = new ApplicationDbContext();
            repo = _context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual List<T> GetAllSearch<TKey>(string key, TKey value)
        {
            var query = _context.Set<T>().AsQueryable();
            if (typeof(TKey) == typeof(string))
                query = query.Where(e => EF.Property<string>(e, key).Contains(value.ToString()));
            else
                query = query.Where(e => EF.Property<TKey>(e, key).Equals(value));
            return query.ToList();
        }

        public virtual T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            return entity;
        }

        public virtual T GetByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            var entity = _context.Set<T>().FirstOrDefault(e => EF.Property<TKey>(e, keyName).Equals(keyValue));
            if (entity == null)
                throw new KeyNotFoundException($"Entity with {keyName} = {keyValue} not found.");
            return entity;
        }

        public virtual bool ExistById(int id)
        {
            return _context.Set<T>().Any(e => EF.Property<int>(e, "Id") == id);
        }

        public virtual bool ExistByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            return _context.Set<T>().Any(e => EF.Property<TKey>(e, keyName).Equals(keyValue));
        }

        public virtual T Add(T entity)
        {
            var entry = _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entry.Entity;
        }

        public virtual T Update(int id, T entity)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            var entry = _context.Entry(existingEntity);
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.Name != "Id")
                {
                    var newValue = property.GetValue(entity);
                    property.SetValue(existingEntity, newValue);
                }
            }

            entry.State = EntityState.Modified;
            _context.SaveChanges();

            var updatedEntity = _context.Set<T>().Find(id);
            if (updatedEntity == null)
                throw new Exception($"Entity with ID {id} not found after update.");
            return updatedEntity;
        }

        public virtual bool Delete(int id)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
                return false;

            _context.Set<T>().Remove(existingEntity);
            _context.SaveChanges();
            return true;
        }
        public virtual bool DeleteByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            var entity = _context.Set<T>()
                .FirstOrDefault(e => EF.Property<TKey>(e, keyName).Equals(keyValue));
            if (entity == null)
                return false;
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public virtual bool DeleteIds(params int[] ids)
        {
            var entities = ids
                .Where(id => ExistById(id))
                .Select(id => _context.Set<T>().Find(id))
                .Where(entity => entity != null)
                .ToList();

            if (!entities.Any())
                return false;

            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
            return true;
        }

        public virtual bool SoftDelete(int id)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
                return false;

            var entityType = existingEntity.GetType();
            var deletedAtProperty = entityType.GetProperty("DeletedAt");
            if (deletedAtProperty == null)
                return false;

            deletedAtProperty.SetValue(existingEntity, DateTime.UtcNow);
            _context.Entry(existingEntity).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        public List<object> GetListFromSqlRaw(string sqlQuery, params object[] parameters)
        {
            var result = new List<object>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                command.CommandType = System.Data.CommandType.Text;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = $"@p{i}";
                        parameter.Value = parameters[i];
                        command.Parameters.Add(parameter);
                    }
                }
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        result.Add(values);
                    }
                }
            }
            return result;
        }
    }
}

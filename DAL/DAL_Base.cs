using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using System.Data.Entity;
using System.Reflection;

namespace DAL
{
    public class DAL_Base<T> where T : class
    {
        private static DAL_Base<T> _instance;
        private static readonly object _lock = new object();

        protected readonly EvermoreBakeryContext _context;
        public DbSet<T> _dto;

        public DAL_Base()
        {
            _context = EvermoreBakeryContext.Instance;
            _dto = _context.Set<T>();
        }

        public static DAL_Base<T> Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DAL_Base<T>();
                    }
                    return _instance;
                }
            }
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }


        public virtual List<T> GetAllSearch<TKey>(string key, TKey value)
        {
            var query = _context.Set<T>().AsQueryable();

            var propertyInfo = typeof(T).GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{key}' does not exist on type '{typeof(T)}'.");

            if (typeof(TKey) == typeof(string))
            {
                query = query.Where(e => propertyInfo.GetValue(e).ToString().Contains(value.ToString()));
            }
            else
            {
                query = query.Where(e => propertyInfo.GetValue(e).Equals(value));
            }

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
            var propertyInfo = typeof(T).GetProperty(keyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{keyName}' does not exist on type '{typeof(T)}'.");

            var entity = _context.Set<T>().FirstOrDefault(e => propertyInfo.GetValue(e).Equals(keyValue));
            if (entity == null)
                throw new KeyNotFoundException($"Entity with {keyName} = {keyValue} not found.");

            return entity;
        }


        public virtual bool ExistById(int id)
        {
            var propertyInfo = typeof(T).GetProperty("Id", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property 'Id' does not exist on type '{typeof(T)}'.");

            return _context.Set<T>().Any(e => (int)propertyInfo.GetValue(e) == id);
        }

        public virtual bool ExistByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            var propertyInfo = typeof(T).GetProperty(keyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{keyName}' does not exist on type '{typeof(T)}'.");

            return _context.Set<T>().Any(e => propertyInfo.GetValue(e).Equals(keyValue));
        }

        public virtual T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
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
            var propertyInfo = typeof(T).GetProperty(keyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{keyName}' does not exist on type '{typeof(T)}'.");

            var entity = _context.Set<T>()
                .FirstOrDefault(e => propertyInfo.GetValue(e).Equals(keyValue));

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

        public List<object[]> GetListFromSqlRaw(string sqlQuery, params object[] parameters)
        {
            var result = new List<object[]>();

            using (var command = _context.Database.Connection.CreateCommand())
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

                if (_context.Database.Connection.State == System.Data.ConnectionState.Closed)
                {
                    _context.Database.Connection.Open();
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        result.Add(values);
                    }
                }

                _context.Database.Connection.Close();
            }

            return result;
        }
    }
}


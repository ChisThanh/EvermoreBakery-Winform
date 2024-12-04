using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using System.Data.Entity;
using System.Reflection;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Base<T> where T : class
    {
        protected readonly EvermoreBakeryContext _context;
        public DbSet<T> _dto;

        public DAL_Base()
        {
            _context = EvermoreBakeryContext.Instance;
            _dto = _context.Set<T>();
        }
       
        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual List<T> GetAllSearch<TKey>(string key, TKey value)
        {
            var query = _context.Set<T>().AsQueryable();

            var propertyInfo = typeof(T).GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{key}' does not exist on type '{typeof(T)}'.");

            if (typeof(TKey) == typeof(string))
                query = query.Where(e => propertyInfo.GetValue(e).ToString().Contains(value.ToString()));
            else
                query = query.Where(e => propertyInfo.GetValue(e).Equals(value));

            return query.ToList();
        }

        public virtual T GetById(long id)
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

        //public virtual T Update(long id, T entity)
        //{
        //    var existingEntity = _context.Set<T>().Find(id);
        //    if (existingEntity == null)
        //        throw new KeyNotFoundException($"Entity with ID {id} not found.");

        //    var entry = _context.Entry(existingEntity);
        //    var properties = typeof(T).GetProperties();

        //    foreach (var property in properties)
        //    {
        //        if (property.Name != "id" || property.Name != "Id")
        //        {
        //            var newValue = property.GetValue(entity);
        //            property.SetValue(existingEntity, newValue);
        //        }
        //    }

        //    entry.State = EntityState.Modified;
        //    _context.SaveChanges();

        //    var updatedEntity = _context.Set<T>().Find(id);
        //    if (updatedEntity == null)
        //        throw new Exception($"Entity with ID {id} not found after update.");
        //    return updatedEntity;
        //}

        public virtual T Update(long id, T entity)
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            var entry = _context.Entry(existingEntity);
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var newValue = property.GetValue(entity);

                if (newValue != null &&
                (!(newValue is ValueType) ||
                 IsValidComparableValue(newValue)))
                {
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


        public virtual bool Delete(long id)
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

        public virtual bool SoftDelete(long id, string property = "deleted_at")
        {
            var existingEntity = _context.Set<T>().Find(id);
            if (existingEntity == null)
                return false;

            var entityType = existingEntity.GetType();
            var deletedAtProperty = entityType.GetProperty(property);
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

        private bool IsValidComparableValue(object value)
        {
            if (value is int || value is long || value is short || value is decimal || value is double || value is float)
                return Convert.ToDecimal(value) > 0;

            if (value is DateTime dateTimeValue)
                return dateTimeValue > DateTime.MinValue; // Kiểm tra DateTime hợp lệ, bạn có thể thay bằng điều kiện khác nếu cần

            return true; // Các kiểu không phải ValueType hoặc không phải số, không cần kiểm tra so sánh
        }
    }
}


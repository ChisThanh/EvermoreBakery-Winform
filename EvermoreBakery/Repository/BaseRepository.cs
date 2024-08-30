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

        public BaseRepository()
        {
            _context = new ApplicationDbContext();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            return entity;
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

    }
}

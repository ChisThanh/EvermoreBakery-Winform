using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_Base<TEntity> where TEntity : class
    {
        protected DAL_Base<TEntity> _dal;
        public BLL_Base()
        {
        }

        public virtual List<TEntity> GetList()
        {
            return _dal.GetAll();
        }

        public virtual TEntity GetByID(int id)
        {
            return _dal.GetById(id);
        }

        public virtual List<TEntity> GetAllSearch<TKey>(string key, TKey value)
        {
            return _dal.GetAllSearch(key, value);
        }

        public virtual TEntity GetById(int id)
        {
            return _dal.GetById(id);
        }

        public virtual TEntity GetByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            return _dal.GetByKeyValue(keyName, keyValue);
        }
        public virtual bool ExistById(int id)
        {
            return _dal.ExistById(id);
        }

        public virtual bool ExistByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            return _dal.ExistByKeyValue(keyName, keyValue);
        }
        public virtual TEntity Add(TEntity entity)
        {
            return _dal.Add(entity);
        }

        public virtual TEntity Update(int id, TEntity entity)
        {
            return _dal.Update(id, entity);
        }

        public virtual bool Delete(int id)
        {
            return _dal.Delete(id);
        }

        public virtual bool DeleteByKeyValue<TKey>(string keyName, TKey keyValue)
        {
            return _dal.DeleteByKeyValue(keyName, keyValue);
        }

        public virtual bool DeleteIds(params int[] ids)
        {
            return _dal.DeleteIds(ids);
        }

        public virtual bool SoftDelete(int id)
        {
            return _dal.SoftDelete(id);
        }

        public List<object[]> GetListFromSqlRaw(string sqlQuery, params object[] parameters)
        {
            return _dal.GetListFromSqlRaw(sqlQuery, parameters);
        }
    }
}
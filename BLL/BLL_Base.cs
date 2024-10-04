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
    public class BLL_Base<T> where T : class
    {
        private static BLL_Base<T> _instance;
        private static readonly object _lock = new object();

        public DAL_Base<T> _dal = DAL_Base<T>.Instance;
        public BLL_Base() { }

        public static BLL_Base<T> Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new BLL_Base<T>();
                    }
                    return _instance;
                }
            }
        }

        public virtual List<T> GetList()
        {
            return _dal.GetAll();
        }


        public virtual T GetByID(int id)
        {
            return _dal.GetById(id);
        }

        public virtual List<T> GetAllSearch<TKey>(string key, TKey value)
        {
            return _dal.GetAllSearch(key, value);
        }

        public virtual T GetById(int id)
        {
            return _dal.GetById(id);
        }

        public virtual T GetByKeyValue<TKey>(string keyName, TKey keyValue)
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

        public virtual T Add(T entity)
        {
            return _dal.Add(entity);
        }

        public virtual T Update(int id, T entity)
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

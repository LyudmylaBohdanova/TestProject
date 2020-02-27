using DLL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        DbContext context;
        IDbSet<T> dbSet;
        public GenericRepository()
        {
            context = new WebApiContext();
            dbSet = context.Set<T>();
        }
        public void Add(T data)
        {
            dbSet.AddOrUpdate(data);
            Save();
        }
        public T Get(int id)
        {
            return dbSet.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }
        public void Remove(T data)
        {
            dbSet.Remove(data);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T data)
        {
            dbSet.AddOrUpdate(data);
            Save();
        }

        ~GenericRepository()
        {
            context.Dispose();
        }
    }
}

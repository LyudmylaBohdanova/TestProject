using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public interface IRepository<T>
    {
        void Save();
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T data);
        void Update(T data);
        void Remove(T data);
    }
}

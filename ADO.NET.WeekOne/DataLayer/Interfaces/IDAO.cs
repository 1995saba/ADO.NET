using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    // Data Access Object
    public interface IDAO<T> where 
        T: class
    {
        void Create(T t);
        void Update(T t);
        void Delete(int id);
        T Read(int id);
        ICollection<T> Read();
    }
}

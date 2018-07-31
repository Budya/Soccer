using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerId.Repositories
{
    public interface IRepository<TClass>  where TClass : class
    {
        Task<IEnumerable<TClass>> GetAllAsync();
        IEnumerable<TClass> GetAll();        
        Task<IEnumerable<TClass>> FindAsync(Func<TClass, Boolean> predicate, string navigationProperty = "");
        IEnumerable<TClass> Find(Func<TClass, Boolean> predicate, string navigationProperty = "");
        Task<TClass> AddAsync(TClass item);
        TClass Add(TClass item);
        Task UpdateAsync(TClass item);
        void Update(TClass item);       
    }
}

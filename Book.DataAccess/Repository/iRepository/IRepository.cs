using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Repository.iRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        T GetFirstOrDefualt(Expression<Func<T,bool>> filter);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}

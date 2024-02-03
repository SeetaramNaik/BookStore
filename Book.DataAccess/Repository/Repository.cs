using Application.DataAccess.Repository.iRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DBSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DBSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            DBSet.Add(entity);  
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DBSet;
            return query.ToList();
        }

        public T GetFirstOrDefualt(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DBSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            DBSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            DBSet.RemoveRange(entity);
        }
    }
}

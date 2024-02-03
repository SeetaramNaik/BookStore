using Application.DataAccess.Repository.iRepository;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db; 
        }

        public void Update(CoverType covertype)
        {
            _db.Update(covertype);
        }
    }
}

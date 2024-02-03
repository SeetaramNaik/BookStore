using Application.DataAccess.Repository.iRepository;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db; 
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.Description = product.Description;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Author = product.Author;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price = product.Price;
                productFromDb.Price50 = product.Price50;
                productFromDb.Price100 = product.Price100;
                productFromDb.CoverTypeId = product.CoverTypeId;
                productFromDb.CategoryId = product.CategoryId;
                //if (product.ImageUrl != null)
                //{
                //    productFromDb.ImageUrl = product.ImageUrl;
                //}
            }
        }
    }
}

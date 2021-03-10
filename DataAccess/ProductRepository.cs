using Milennium.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    /// <summary>
    /// todo: class is not thread safe
    /// </summary>
    public class ProductRepository : IRepository<Product>
    {
        private List<Product> _products = new List<Product>();

        public ProductRepository()
        {
            _products.Add(new Product() { Id = 1, Name = "Monitor", Price = 1500m });

            _products.Add(new Product() { Id = 2, Name = "Computer", Price = 7500m });

            _products.Add(new Product() { Id = 3, Name = "Keyboard", Price = 60m });
        }
        public void Add(Product product)
        {
            product.Id = _products.Any() ? _products.Max(x => x.Id) + 1 : 1;
            _products.Add(product);
        }

        public void Remove(Product product)
        {
            _products.RemoveAll(x => x.Id == product.Id);
        }

        public Product Get(int id)
        {
            return _products.Where(x => x.Id == id).SingleOrDefault();
        }

        public List<Product> Get()
        {
            return _products.ToList();
        }

        public void Update(Product product)
        {
            var entity = _products.Where(x => x.Id == product.Id).SingleOrDefault();

            if (entity == null)
            {
                throw new Exception($"Product with Id not found");
            }
            entity.Name = product.Name;
            entity.Price = product.Price;
        }
    }
}

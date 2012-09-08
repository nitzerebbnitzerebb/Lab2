using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Context;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;

namespace SportsStore.Domain.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products { get { return context.Products; } }

        public void Save(Product product)
        {
            if (product.ProductID == 0)
                context.Products.Add(product);
            else
                context.Entry(product).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}

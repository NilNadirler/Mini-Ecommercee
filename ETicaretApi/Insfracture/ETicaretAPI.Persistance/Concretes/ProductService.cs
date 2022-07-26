using ETicaretDbContext.Application.Abstraction;
using ETicaretDbContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretDbContext.Persistance.Concrete
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}

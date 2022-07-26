using ETicaretDbContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretDbContext.Application.Abstraction
{
    public interface IProductService
    {

        List<Product> GetProducts();
    }
}

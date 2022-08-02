using ETicaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ETicaretDbContext.Domain.Entities;

namespace ETicaretAPI.Application.Features.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        readonly IProductReadRepository _productReadRepository;

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
             P.Product product = await _productReadRepository.GetByIdAsync(request.Id,false);

            return new()
            {
                Stock = product.Stock,
                Price = product.Price,
                Name = product.Name,
            };
        }
    }
}

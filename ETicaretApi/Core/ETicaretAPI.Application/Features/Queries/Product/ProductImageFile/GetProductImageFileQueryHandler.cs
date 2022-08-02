using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ETicaretDbContext.Domain.Entities;


namespace ETicaretAPI.Application.Features.Queries.Product.ProductImageFile
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImageFileQueryResponse>> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(
               p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));


            return product.ProductImageFiles.Select(
                p => new GetProductImageFileQueryResponse
                {
                    Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                    FileName = p.FileName,  
                    Id = p.Id
                }).ToList();
        }
    }
}

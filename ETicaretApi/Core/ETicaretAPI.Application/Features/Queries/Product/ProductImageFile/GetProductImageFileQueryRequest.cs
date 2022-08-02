using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.ProductImageFile
{
    public class GetProductImageFileQueryRequest : IRequest< List<GetProductImageFileQueryResponse>>
    {
        public string Id { get; set; }
    }
}

using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UpdateProductImage;
using ETicaretAPI.Application.Features.Queries.GetByIdProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.ProductImageFile;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretDbContext.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes="Admin")]
    public class ProductsController : Controller
    {
       
        private IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task <IActionResult>Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);    

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);

            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);

            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpPut]

        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest )
        {

            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }


        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
          
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);    

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {

            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse  response= await _mediator.Send(uploadProductImageCommandRequest);

            return Ok();    
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageFileQueryRequest queryRequest)
        {
            List<GetProductImageFileQueryResponse> response = await _mediator.Send(queryRequest);

            return Ok(response);

        }



        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest commandRequest, [FromQuery] string imageId)
        {


            commandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(commandRequest);

            return Ok();
        }
    }
}

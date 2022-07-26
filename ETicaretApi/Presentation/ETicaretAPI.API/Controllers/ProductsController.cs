﻿using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretDbContext.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretDbContext.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IFileService _fileService;

 

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _fileService = fileService;
        }




        //[HttpGet]
        //public async Task GetProducts()
        //{
        //    await _productWriteRepository.AddRangeAsync(new()
        //    {
        //        new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreateDate = DateTime.UtcNow, Stock = 10 },
        //        new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreateDate = DateTime.UtcNow, Stock = 20 },
        //        new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreateDate = DateTime.UtcNow, Stock = 130 },

        //    });

        //    var count = await _productWriteRepository.SaveAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    Product product = await _productReadRepository.GetByIdAsync(id);

        //    return Ok(product);
        //}


        [HttpGet]
        public async Task <IActionResult>Get([FromQuery] Pagination pagination)
        {

          
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdatedDate
            }). ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }



        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {



           await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });



            await _productWriteRepository.SaveAsync();
            //return StatusCode((int)HttpStatusCode.Created);

            return Ok(model);
        }

        [HttpPut]

        public async Task<IActionResult> Put(VM_Update_Model model)
        {

            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;  
            product.Price = model.Price;    
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);

            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("product-images", Request.Form.Files);
            return Ok();    
        }
    }
}
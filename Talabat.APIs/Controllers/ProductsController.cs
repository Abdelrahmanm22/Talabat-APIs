using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,IMapper mapper)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
        }
        // Get All Products
        [HttpGet]
        //BaseURL/api/Products  ==> Get method
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecifications();
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
            return Ok(MappedProducts);
        }



        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //var Product = await _productRepo.GetByIdAsync(id);
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MappedProduct);
        }

    }
}

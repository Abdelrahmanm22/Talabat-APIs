using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
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
        [Authorize]
        [HttpGet]
        //BaseURL/api/Products  ==> Get method
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(Params);
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var CountSpec = new ProductWithFiltrationForCountAsync(Params);
            var Count = await _productRepo.GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDto>(Params.PageSize,Params.PageIndex,Count,MappedProducts));
        }



        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //var Product = await _productRepo.GetByIdAsync(id);
            var Spec = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (Product is null) return NotFound(new ApiResponse(404, "Product Not Found"));
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MappedProduct);
        }



    }
}

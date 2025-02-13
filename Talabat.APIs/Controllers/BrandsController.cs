using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : APIBaseController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public BrandsController(IGenericRepository<ProductBrand> ProductBrandRepo)
        {
            _productBrandRepo = ProductBrandRepo;
        }


        //Get All Brands
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _productBrandRepo.GetAllAsync();
            return Ok(Brands);
        }
    }
}

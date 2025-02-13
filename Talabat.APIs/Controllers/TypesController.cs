using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class TypesController : APIBaseController
    {
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public TypesController(IGenericRepository<ProductType> ProductTypeRepo)
        {
            _productTypeRepo = ProductTypeRepo;
        }
        //Get All Types
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _productTypeRepo.GetAllAsync();
            return Ok(Types);
        }


    }
}

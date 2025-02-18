using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories
{
    public interface IBasketRepository
    {
        //Get Basket
        Task<CustomerBasket?> GetBasketAsync(string basketId);

        //Update Basket
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        //Delete Basket
        Task<bool> DeleteBasketAsync(string basketId);
    }
}

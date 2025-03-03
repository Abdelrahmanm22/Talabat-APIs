using AutoMapper;

using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductType,O=>O.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.ProductBrand,O=>O.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.PictureURL,O=>O.MapFrom<ProductPictureUrlResolver>());


            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.Order_Aggregate.Address>();
        }
    }
}

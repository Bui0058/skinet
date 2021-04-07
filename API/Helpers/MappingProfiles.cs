using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
                //configure AutoMapper for returing samename but different type 
                //between the source & desnitation
                //d: destination; o: option; s: source.
                //the 3 ForMember use the Resolver class to add url to PictureUr.
            CreateMap<Address, AddressDto>().ReverseMap();
                //we don't need to do configuration because the properties name  
                //are exact match to each other.
                //With ReversMap() this  can also map the orther way
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
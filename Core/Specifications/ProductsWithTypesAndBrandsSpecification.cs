using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
                    : base(x =>  //this expression is for filtering feature
                        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                        (productParams.Search)) && 
                        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
                    ) 
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort)) //check the sort option to act accordingly
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        // this constructor is for get specific product with given id
        // the base(x => x.Id == id) is the initial value of the criteria property 
        // which is an expression in the constructor of BaseSpecification
        // This constructor will set up the instance of its parent
        public ProductsWithTypesAndBrandsSpecification(int id) 
                : base(x => x.Id == id)  
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
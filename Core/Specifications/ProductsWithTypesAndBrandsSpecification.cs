using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
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
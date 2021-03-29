using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> 
    {
        Expression<Func<T, bool>> Criteria {get;} //this is our where creteria
        List<Expression<Func<T, object>>>  Includes {get;} //this is for the list include statement
         
    }
}
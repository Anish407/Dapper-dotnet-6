using Dapper.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.DAL.Core
{
    public interface IProductRepository
    {
        //get all products-- as in the db
        Task<IEnumerable<Product>> GetAllProducts();

        //get product name-- model and db column names vary
        Task<IEnumerable<ProductWIthDIfferentName>> GetAllProductsNames();
    }
}
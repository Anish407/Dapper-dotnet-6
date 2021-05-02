using Dapper.DAL.Core;
using Dapper.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dapper.DAL.Queries.ProductQueries;

namespace Dapper.DAL.Infra
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(IDapperExecutor dapperExecutor) => DapperExecutor = dapperExecutor;

        public IDapperExecutor DapperExecutor { get; }


        public async Task<IEnumerable<Product>> GetAllProducts()
            => await DapperExecutor.GetAll<Product>(GetAllProductsQuery);

        //When the property names in the model are different from the db columns
        public async Task<IEnumerable<ProductWIthDIfferentName>> GetAllProductsNames() 
            => await DapperExecutor.GetAll<ProductWIthDIfferentName>(GetAllProductWithProductNamesQuery);
    }
}

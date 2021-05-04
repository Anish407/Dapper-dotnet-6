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

        /// <summary>
        /// Use this when the property names in the model differ from the db column names
        /// In this case the Name column in the DB is mapped to the ProductName property in the model
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductWIthDIfferentName>> GetAllProductsNames() 
            => await DapperExecutor.GetAll<ProductWIthDIfferentName>(GetAllProductWithProductNamesQuery);
    }
}

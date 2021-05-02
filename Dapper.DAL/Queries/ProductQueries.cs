namespace Dapper.DAL.Queries
{
    public class ProductQueries
    {
        public const string GetAllProductsQuery = "select Top 10  * from Production.Product";
        public const string GetAllProductWithProductNamesQuery = "select Name as ProductName from Production.Product";
    }
}

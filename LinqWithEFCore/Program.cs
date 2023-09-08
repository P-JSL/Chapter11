using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Console;

FilterAndSort();
//JoinCategoriesAndProducts();
//GrouupJoinCategoriesAndProducts();
//AggregateProducts();
static void FilterAndSort()
{
    using (Northwind db = new())
    {
        DbSet<Product> allProducts = db.Products;
        
        IQueryable<Product> processedProducts = allProducts.processSequence();
        IQueryable<Product> filteredProducts = processedProducts.Where(product => product.UnitPrice < 10M);
        IOrderedQueryable<Product> sortedAndFilteredProducts = filteredProducts.OrderByDescending(product => product.UnitPrice);
        var projectedProducts = sortedAndFilteredProducts.Select(product => new
        {
            product.ProductId,
            product.ProductName,
            product.UnitPrice
        }) ;

        WriteLine("Products that cost less than $10.");
        //foreach(Product p in sortedAndFilteredProducts)
        foreach(var p in projectedProducts)
        {
            WriteLine("{0}: {1} costs {2:$#,##0,00}",
                p.ProductId,p.ProductName,p.UnitPrice);
        }
        WriteLine();
    }
}

static void JoinCategoriesAndProducts()
{
    using (Northwind db = new())
    {
        var queryJoin = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: Category => Category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) => new
            {
                c.CategoryName,
                p.ProductName,
                p.ProductId
            });
        foreach(var item in queryJoin)
        {
            WriteLine("{0}: {1} is in {2}",
                item.ProductId,
                item.ProductName,
                item.CategoryName);
        }
    }
}

static void GrouupJoinCategoriesAndProducts()
{
    using (Northwind db = new())
    {
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, matchingProducts) => new
            {
                c.CategoryName,
                products = matchingProducts.OrderBy(p => p.ProductName)
            }) ;
        foreach(var category in queryGroup) 
        {
            WriteLine("{0} has {1} products",
                category.CategoryName,
                category.products.Count());
            foreach(var product in category.products)
            {
                WriteLine($"    {product.ProductName}");
            }
        };
    }
}

static void AggregateProducts()
{
    using (Northwind db = new())
    {
        WriteLine("{0,-25}  {1,10}",
            "Product count:", db.Products.Count());
        WriteLine("{0,-25}  {1,10:$#,##0,00}",
            "Highest product price:",
            db.Products.Max(p => p.UnitPrice));
        WriteLine("{0,-25}  {1,10:N0}",
            "Sum of units in stock:",db.Products.Sum(p => p.UnitsInStock));
        WriteLine("{0,-25}  {1,10:N0}",
            "Sum of units on order:", db.Products.Sum(p => p.UnitsOnOrder));
        WriteLine("{0,-25}  {1,10:$#,##0,00}",
            "Average product price:",
            db.Products.Average(p => p.UnitPrice));
        WriteLine("{0,-25}  {1,10:$#,##0,00}",
            "Value product price:",
            db.Products.Sum(p => p.UnitPrice*p.UnitsInStock));
    }
}
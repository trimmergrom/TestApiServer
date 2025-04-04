using Microsoft.EntityFrameworkCore;

using TestApiServer.Domain;
using TestApiServer.Persistence.Common.Exceptions;
using TestApiServer.Persistence.Dto.Product.Commands;
using TestApiServer.Persistence.Dto.Product.Queries;
using TestApiServer.Persistence.Dto.ProductCategory.Queries;
using TestApiServer.Persistence.Repositories.Interfaces;


namespace TestApiServer.Persistence.Repositories
{
    public class RepositoryProduct(TestApiServerDbContext context, IRepositoryProductCategory repositoryProductCategory):IRepositoryProduct
    {
        public async Task<List<AllProduct>> GetAllAsync()
        {
            var products = await context.Products
                .Select(product => new AllProduct
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                })
                .AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
        {
            var products = await context.Products
                .Select(product => new RangeProduct
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                })
                .AsNoTracking()
                .Skip(countSkip)
                .Take(countTake)
                .ToListAsync();
            return products;
        }

       

        public async Task<int> AddAsync(CreateProduct createProduct)
        {
            var productCategory = await repositoryProductCategory
                .GetByIdAsync(createProduct.IdProductCategory);

            var product = new Product
            {
                Name = createProduct.Name,
                Description = createProduct.Description,
                Price = createProduct.Price,                
                ProductCategory = productCategory,              
            };
            
            await context.AddAsync(product);            
            await context.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await context.Products.SingleOrDefaultAsync(product => product.Id == id)
                ?? throw new NotFoundException(nameof(Product), id);
            context.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var updatedProduct = await context.Products
                .SingleOrDefaultAsync(x => x.Id == product.Id)
                ?? throw new NotFoundException(nameof(Product), product.Id);
            updatedProduct.Name = product.Name;
            updatedProduct.Description = product.Description;
            updatedProduct.Price = product.Price;

            var productCategory = await context.ProductCategories.SingleOrDefaultAsync(productCategory => productCategory.Id == product.ProductCategoryId)
                ?? throw new NotFoundException(nameof(ProductCategory), product.ProductCategoryId);
            updatedProduct.ProductCategory = productCategory;
            await context.SaveChangesAsync();
        }

        public async Task<DetailsProduct> GetDetailsByIdAsync(int id)
        {
            var product = await context.Products
                .Include(product => product.ProductCategory)
                .Select(product => new DetailsProduct
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    IdProductCategory = product.ProductCategoryId,
                    ProductCategoryName = product.ProductCategory.Name,
                    ProductCategoryDescription = product.ProductCategory.Description
                })
                .AsNoTracking()
                .SingleOrDefaultAsync(product => product.Id == id)
                ?? throw new NotFoundException(nameof(Product), id);
            //if(productCategory == null)
            //{
            //    throw new NotFoundException(nameof(ProductCategory), id);
            //}
            return product;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await context.Products.SingleOrDefaultAsync(product => product.Id == id)
                                  ?? throw new NotFoundException(nameof(Product), id);

            return product;

        }


        public async Task UpdateAsync(UpdateProduct updateProduct)
        {
            var updatedProduct = await GetByIdAsync(updateProduct.Id);
            updatedProduct.Name = updateProduct.Name;
            updatedProduct.Description = updateProduct.Description;
            updatedProduct.Price = updateProduct.Price;

            var productCategory = await context.ProductCategories
                .SingleOrDefaultAsync(productCategory => productCategory.Id == updateProduct.IdProductCategory)
                ?? throw new NotFoundException(nameof(ProductCategory), updateProduct.IdProductCategory);
            updatedProduct.ProductCategory = productCategory;
            await context.SaveChangesAsync();

        }
    }
}

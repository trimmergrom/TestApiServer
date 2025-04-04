using Microsoft.EntityFrameworkCore;

using TestApiServer.Domain;
using TestApiServer.Persistence.Common.Exceptions;
using TestApiServer.Persistence.Dto.ProductCategory.Commands;
using TestApiServer.Persistence.Dto.ProductCategory.Queries;
using TestApiServer.Persistence.Repositories.Interfaces;

namespace TestApiServer.Persistence.Repositories
{
    public class RepositoryProductCategory(TestApiServerDbContext context) : IRepositoryProductCategory
    {
        public async Task<List<ProductCategoryAll>> GetAllAsync()
        {
            var productCategories = await context.ProductCategories
                .Select(productCategory => new ProductCategoryAll
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name,

                })
                .AsNoTracking().ToListAsync();
            return productCategories;
        }

        public async Task<DetailsProductCategory> GetDetailsByIdAsync(int id)
        {
            var productCategory = await context.ProductCategories
                .Select(productCategory => new DetailsProductCategory
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name,
                    Description = productCategory.Description,
                })
                .AsNoTracking().SingleOrDefaultAsync(productCategory => productCategory.Id == id)
                ?? throw new NotFoundException(nameof(ProductCategory), id);
            //if(productCategory == null)
            //{
            //    throw new NotFoundException(nameof(ProductCategory), id);
            //}
            return productCategory;
        }

        public async Task<int> AddAsync(ProductCategory productCategory)
        {
            await context.AddAsync(productCategory);
            await context.SaveChangesAsync();
            return productCategory.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var productCategory = await context.ProductCategories.AsNoTracking().SingleOrDefaultAsync(productCategory => productCategory.Id == id)
                ?? throw new NotFoundException(nameof(ProductCategory), id);
            context.ProductCategories.Remove(productCategory);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCategory productCategory)
        {
            var updatedProductCategory = await context.ProductCategories
                .SingleOrDefaultAsync(x => x.Id == productCategory.Id)
                ?? throw new NotFoundException(nameof(ProductCategory), productCategory.Id);
            updatedProductCategory.Name = productCategory.Name;
            updatedProductCategory.Description = productCategory.Description;

            //await context.AddAsync(updatedProductCategory);
            await context.SaveChangesAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            var productCategory = await context.ProductCategories
                                  .SingleOrDefaultAsync(productCategory => productCategory.Id == id)
                                  ?? throw new NotFoundException(nameof(ProductCategory), id);

            return productCategory;

        }
        

    }    
        
}

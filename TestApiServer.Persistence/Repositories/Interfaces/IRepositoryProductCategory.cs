using System;
using System.Collections.Generic;
using System.Linq;
using TestApiServer.Domain;
using TestApiServer.Persistence.Dto.ProductCategory.Commands;
using TestApiServer.Persistence.Dto.ProductCategory.Queries;

namespace TestApiServer.Persistence.Repositories.Interfaces
{
    public interface IRepositoryProductCategory
    {
        Task<List<ProductCategoryAll>> GetAllAsync();
        Task<DetailsProductCategory> GetDetailsByIdAsync(int id);
        Task<ProductCategory>GetByIdAsync(int id);
        Task<int> AddAsync(ProductCategory productCategory);
        Task DeleteAsync(int id);
        Task UpdateAsync(ProductCategory productCategory);
    }
}

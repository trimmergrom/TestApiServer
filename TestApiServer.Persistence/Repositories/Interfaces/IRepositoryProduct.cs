using TestApiServer.Domain;
using TestApiServer.Persistence.Dto.Product.Commands;
using TestApiServer.Persistence.Dto.Product.Queries;

namespace TestApiServer.Persistence.Repositories.Interfaces
{
    public interface IRepositoryProduct
    {
        Task<List<AllProduct>> GetAllAsync();
        Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake);
        Task<DetailsProduct> GetDetailsByIdAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task<int> AddAsync(CreateProduct product);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProduct product);
    }
}

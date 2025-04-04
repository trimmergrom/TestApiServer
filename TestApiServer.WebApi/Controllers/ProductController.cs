using Microsoft.AspNetCore.Mvc;
using TestApiServer.Domain;
using TestApiServer.Persistence.Dto.Product.Commands;
using TestApiServer.Persistence.Dto.Product.Queries;
using TestApiServer.Persistence.Repositories.Interfaces;

namespace TestApiServer.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IRepositoryProduct repositoryProduct) : ControllerBase
{
    [HttpGet(Name = "GetAllPorduct")]
    [ActionName(nameof(GetAllAsync))]
    public async Task<List<AllProduct>> GetAllAsync()
    {
        var product = await repositoryProduct.GetAllAsync();
        return product;
    }

    [HttpGet("{countSkip}/{countTake}", Name = "GetRangePorduct")]
    [ActionName(nameof(GetRangeAsync))]
    public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeProducts = await repositoryProduct.GetRangeAsync(countSkip, countTake);
        return rangeProducts;
    }

    [HttpGet("{id}", Name = "GetByIdProduct")]
    [ActionName(nameof(GetDetailsAsync))]
    public async Task<DetailsProduct> GetDetailsAsync(int id)
    {
        var product = await repositoryProduct.GetDetailsByIdAsync(id);
        return product;
    }

    [HttpPost(Name = "AddProduct")]
    [ActionName(nameof(AddAsync))]
    public async Task<int> AddAsync([FromBody]CreateProduct createProduct)
    {
        var id = await repositoryProduct.AddAsync(createProduct);
        return id;
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ActionName(nameof(DeleteAsync))]
    public async Task DeleteAsync(int id)
    {
        await repositoryProduct.DeleteAsync(id);
        //return productCategory;
    }

    [HttpPut(Name = "UpdateProduct")]
    [ActionName(nameof(UpdateAsync))]
    public async Task UpdateAsync([FromBody]UpdateProduct updateProduct)
    {
        await repositoryProduct.UpdateAsync(updateProduct);
        
    }
}
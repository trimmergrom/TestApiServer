using Microsoft.AspNetCore.Mvc;
using TestApiServer.Domain;
using TestApiServer.Persistence.Repositories.Interfaces;
using TestApiServer.Persistence.Dto.ProductCategory.Commands;
using TestApiServer.Persistence.Dto.ProductCategory.Queries;
using FluentValidation;
using System.Text.Json;

namespace TestApiServer.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductCategoriesController(IRepositoryProductCategory repositoryProductCategory,
    IValidator<CreateProductCategory> validatorProductCategory) : ControllerBase
{
    [HttpGet(Name ="GetAllProductCategory")]
    public async Task<List<ProductCategoryAll>>GetAllAsync()
    {
        var productCategories = await repositoryProductCategory.GetAllAsync();
        return productCategories;
    }

    [HttpGet("{id}", Name ="GetByIdProductCategory")]
    public async Task<DetailsProductCategory>GetByIdAsync(int id)
    {
        var productCategory = await repositoryProductCategory.GetDetailsByIdAsync(id);
        return productCategory;
    }

    [HttpPost(Name = "AddProductCategory")]
    public async Task<ActionResult<int>> AddAsync([FromBody]CreateProductCategory createProductCategory)
    {       
        validatorProductCategory.ValidateAndThrow(createProductCategory);
        var productCategory = new ProductCategory
        {
            Name = createProductCategory.Name,
            Description = createProductCategory.Description,
        };

        var id = await repositoryProductCategory.AddAsync(productCategory);
        return Ok(id);   
    }

    [HttpDelete("{id}", Name ="DeleteProductCategory")]
    public async Task DeleteAsync(int id)
    {
        await repositoryProductCategory.DeleteAsync(id);
        //return productCategory;
    }

    [HttpPut(Name = "UpdateProductCategory")]
    public async Task UpdateAsync([FromBody]ProductCategory productCategory)
    {
        await repositoryProductCategory.UpdateAsync(productCategory);
       
    }
}

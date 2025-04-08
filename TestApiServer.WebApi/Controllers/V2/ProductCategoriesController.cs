using Microsoft.AspNetCore.Mvc;
using TestApiServer.Domain;
using TestApiServer.Persistence.Repositories.Interfaces;
using TestApiServer.Persistence.Dto.ProductCategory.Commands;
using TestApiServer.Persistence.Dto.ProductCategory.Queries;
using FluentValidation;
using System.Text.Json;
using Asp.Versioning;

namespace TestApiServer.WebApi.Controllers.V2;

[ApiController]
//[ControllerName("productCategories")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]

public class ProductCategoriesController(IRepositoryProductCategory repositoryProductCategory,
    IValidator<CreateProductCategory> validatorProductCategory, IValidator<UpdateProductCategory> validatorUpdateProductCategory) : ControllerBase
{
    ///// <summary>
    ///// Get all product category
    ///// </summary>
    ///// <remarks>
    ///// Simple request:
    ///// GET/api/productCategories
    ///// </remarks>
    ///// <returns>
    ///// returns List of product categories (productCategories)
    ///// </returns>
    ///// <response code = "200"> Success</response>
    //[HttpGet(Name ="GetAllProductCategory")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<List<ProductCategoryAll>>GetAllAsync()
    //{
    //    var productCategories = await repositoryProductCategory.GetAllAsync();
    //    return productCategories;
    //}

    /// <summary>
    /// Get product category by id
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// GET/api/productCategories/id
    /// </remarks>
    /// <returns>
    /// returns product category (productCategory)
    /// </returns>
    /// <response code = "200"> Success</response>
    /// <response code = "404"> If productCategory is not found</response>
    [HttpGet("{id}", Name ="GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailsProductCategory>GetByIdAsync(int id)
    {
        var productCategory = await repositoryProductCategory.GetDetailsByIdAsync(id);
        return productCategory;
    }

    /// <summary>
    /// Creates product category
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// POST/api/productCategories
    /// {
    ///     name: "name of product category",
    ///     description: "description of product category
    /// }
    /// </remarks>
    /// <param name="createProductCategory">Create ProductCategory object</param>
    /// <returns>
    /// returns id (int)
    /// </returns>
    /// <response code = "200"> Success</response>
    /// <response code = "400"> If name is empty or length exeeds 100 character,
    /// If descriptions is empty or length exeeds 400 character
    /// </response>
    [HttpPost(Name = "Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Deletes product category
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// DELETE/api/productCategories/id
    /// </remarks>
    /// <returns></returns>
    /// <response code = "200"> Success</response>
    [HttpDelete("{id}", Name ="Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteAsync(int id)
    {
        await repositoryProductCategory.DeleteAsync(id);
        //return productCategory;
    }

    /// <summary>
    /// Updates product category
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// PUT/api/productCategories/id
    /// </remarks>
    /// <returns></returns>
    /// <response code = "200"> Success</response>
    [HttpPut(Name = "Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateAsync([FromBody]UpdateProductCategory updateProductCategory)
    {
       validatorUpdateProductCategory.ValidateAndThrow(updateProductCategory);
        var productCategory = new ProductCategory
        {
            Id = updateProductCategory.Id,
            Name = updateProductCategory.Name,
            Description = updateProductCategory.Description
        };
        await repositoryProductCategory.UpdateAsync(productCategory);
       
    }
}

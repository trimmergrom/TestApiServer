using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestApiServer.Domain;
using TestApiServer.Persistence.Dto.Product.Commands;
using TestApiServer.Persistence.Dto.Product.Queries;
using TestApiServer.Persistence.Dto.ProductCategory.Commands;
using TestApiServer.Persistence.Repositories.Interfaces;

namespace TestApiServer.WebApi.Controllers.V1;

[ApiController]
//[ControllerName("Products")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController(IRepositoryProduct repositoryProduct,
    IValidator<CreateProduct> validatorProduct, IValidator<UpdateProduct> validatorUpdateProduct) : ControllerBase
{

    /// <summary>
    /// Get all product
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// GET/api/Products
    /// </remarks>
    /// <returns>
    /// returns List of product (product)
    /// </returns>
    /// <response code = "200"> Success</response>
    [HttpGet(Name = "GetAllProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ActionName(nameof(GetAllAsync))]
    public async Task<List<AllProduct>> GetAllAsync()
    {
        var product = await repositoryProduct.GetAllAsync();
        return product;
    }
    /// <summary>
    /// Get range product
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// GET/api/Products/countSkip/countTake
    /// </remarks>
    /// <returns>
    /// returns List of product (product)
    /// </returns>
    /// <response code = "200"> Success</response>
    [HttpGet("{countSkip}/{countTake}", Name = "GetRange")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ActionName(nameof(GetRangeAsync))]
    public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeProducts = await repositoryProduct.GetRangeAsync(countSkip, countTake);
        return rangeProducts;
    }

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// GET/api/Products/id
    /// </remarks>
    /// <returns>
    /// returns product (product)
    /// </returns>
    /// <response code = "200"> Success</response>
    /// <response code = "404"> If product is not found</response>
    [HttpGet("{id}", Name = "GetByIdProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName(nameof(GetDetailsAsync))]
    public async Task<DetailsProduct> GetDetailsAsync(int id)
    {
        var product = await repositoryProduct.GetDetailsByIdAsync(id);
        return product;
    }

    /// <summary>
    /// Creates product
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// POST/api/Products
    /// {       
    ///     name: "name of product",
    ///     description: "description of product",
    ///     price: "price of product",
    ///     IdProductCategory: " id of category of product"
    /// }
    /// </remarks>
    /// <param name="createProduct">Create Product object</param>
    /// <returns>
    /// returns id (int)
    /// </returns>
    /// <response code = "200"> Success</response>
    /// <response code = "400"> If name is empty or length exeeds 100 character,
    /// If descriptions is empty or length exeeds 400 character,
    /// If price less or equals 0 or price exceed 100000,
    /// If IdProductCategory is empty
    /// </response>
    /// <response code ="404">If id of product is empty</response>
    [HttpPost(Name = "AddProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName(nameof(AddAsync))]
    public async Task<int> AddAsync([FromBody]CreateProduct createProduct)
    {
        validatorProduct.ValidateAndThrow(createProduct);
        var id = await repositoryProduct.AddAsync(createProduct);
        return id;
    }

    /// <summary>
    /// Deletes product
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// DELETE/api/Products/id
    /// </remarks>
    /// <returns></returns>
    /// <response code = "200"> Success</response>
    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ActionName(nameof(DeleteAsync))]
    public async Task DeleteAsync(int id)
    {
        await repositoryProduct.DeleteAsync(id);        
    }

    /// <summary>
    /// Updates product
    /// </summary>
    /// <remarks>
    /// Simple request:
    /// PUT/api/Products/id
    /// {       
    ///     id: "id of product",
    ///     name: "name of product",
    ///     description: "description of product",
    ///     price: "price of product",
    ///     IdProductCategory: " id of category of product"
    /// }
    /// </remarks>
    /// <param name="updateProduct">Create Product object</param>
    /// <returns></returns>
    /// <response code = "200"> Success</response>
    /// <response code = "400"> If name is empty or length exeeds 100 character,
    /// If descriptions is empty or length exeeds 400 character,
    /// If price less or equals 0 or price exceed 100000,
    /// If IdProductCategory is empty
    /// </response>
    /// <response code ="404">If id of product is empty</response>
    [HttpPut(Name = "UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName(nameof(UpdateAsync))]
    public async Task UpdateAsync([FromBody]UpdateProduct updateProduct)
    {
        validatorUpdateProduct.ValidateAndThrow(updateProduct);
        await repositoryProduct.UpdateAsync(updateProduct);
        
    }
}
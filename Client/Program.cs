using System.Net.Http.Json;
using Client;

//ProductCategory_GetAll

var responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7020/api/v1/ProductCategories");
var productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<ProductCategoryAll>>();
foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}
Console.ReadLine();


////////////////////////////////////////////////////////////////////////////////////////////////
//ProductCategory_GetById_GET

Console.WriteLine("Input Id of ProductCategory for call objects");
int id = int.Parse(Console.ReadLine());
var responseGetProductCategory = await new HttpClient().GetAsync($"https://localhost:7020/api/v1/ProductCategories/{id}");
var currentProductCategory = await responseGetProductCategory.Content.ReadFromJsonAsync<DetailsProductCategory>();
Console.WriteLine($"Id: {currentProductCategory.Id} Name: {currentProductCategory.Name} " +
    $" Description: {currentProductCategory.Description}");

//////////////////////////////////////////////////////////////////////////////////////////////////////
//DELETE

Console.WriteLine("Input Id of ProductCategory for DELETE");
id = int.Parse(Console.ReadLine());
await new HttpClient().DeleteAsync($"https://localhost:7020/api/v1/ProductCategories/{id}");

///////////////////////////////////////////////////////////////////////////////////////////////////////
//ProductCategory_ADD-POST

Console.WriteLine("Enter Name of ProductCategory for Add");
string name = Console.ReadLine();
Console.WriteLine("Enter description of ProductCategory for Add");
string description = Console.ReadLine();
var createProductCategory = new CreateProductCategory
{
    Name = name,
    Description = description
};

responseGetProductCategories = await new HttpClient().PostAsJsonAsync("https://localhost:7020/api/v1/ProductCategories", createProductCategory);
var idProductCategory = await responseGetProductCategories.Content.ReadFromJsonAsync<int>();

responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7020/api/v1/ProductCategories");
productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<ProductCategoryAll>>();
foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
//ProductCategory_Update-PUT

Console.WriteLine("Input Id of ProductCategory for PUT");
int id_put = int.Parse(Console.ReadLine());
Console.WriteLine("Enter Name of ProductCategory for PUT");
string name_put = Console.ReadLine();
Console.WriteLine("Enter description of ProductCategory for PUT");
string description_put = Console.ReadLine();
var updatedProductCategory = new UpdateProductCategory
{
    Id = id_put,
    Name = name_put,
    Description = description_put
};
responseGetProductCategories = await new HttpClient().PutAsJsonAsync("https://localhost:7020/api/v1/ProductCategories", updatedProductCategory);

responseGetProductCategories = await new HttpClient().GetAsync("https://localhost:7020/api/v1/ProductCategories");
productCategories = await responseGetProductCategories.Content.ReadFromJsonAsync<List<ProductCategoryAll>>();
foreach (var productCategory in productCategories)
{
    Console.WriteLine($"Id: {productCategory.Id} Name: {productCategory.Name}");
}

//////////////////////////////////////////////////Products///////////////////////////////////////////
//AllProduct-GET
Console.WriteLine("OutPut  AllProduct ");
var responseGetProduct = await new HttpClient().GetAsync("https://localhost:7020/api/v1/Product");
var products = await responseGetProduct.Content.ReadFromJsonAsync<List<AllProduct>>();
foreach (var product in products)
{
    Console.WriteLine($"Id: {product.Id} Name: {product.Name}");   
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////
//GetById-GET
Console.WriteLine("Input Id of Product for call objects");
int id_call = int.Parse(Console.ReadLine());
var responseGetProduct1 = await new HttpClient().GetAsync($"https://localhost:7020/api/v1/Product/{id_call}");
var currentProduct = await responseGetProduct1.Content.ReadFromJsonAsync<DetailsProduct>();
Console.WriteLine($"Id: {currentProduct.Id} Name: {currentProduct.Name} Description: {currentProduct.Description} " +
    $"Price: {currentProduct.Price} IdProductCategory: {currentProduct.IdProductCategory} " +
    $"ProductCategoryName: {currentProduct.ProductCategoryName} " +
    $"ProuctCategoryDescription: {currentProduct.ProductCategoryDescription}");

//DELETE
Console.WriteLine("Input Id of Product for DELETE");
int id_p = int.Parse(Console.ReadLine());
await new HttpClient().DeleteAsync($"https://localhost:7020/api/v1/Product/{id_p}");

//ADD-POST
Console.WriteLine("Enter Name of Product for Add");
string name_add = Console.ReadLine();
Console.WriteLine("Enter description of Product for Add");
string description_add = Console.ReadLine();
Console.WriteLine("Enter Pricte of Product for Add");
decimal price_add = decimal.Parse(Console.ReadLine());
Console.WriteLine("Enter IdProductCategory of Product for Add");
int id_pcadd = int.Parse(Console.ReadLine());

var createProduct = new CreateProduct
{
    Name = name_add,
    Description = description_add,
    Price = price_add,
    IdProductCategory = id_pcadd

};

responseGetProduct = await new HttpClient().PostAsJsonAsync("https://localhost:7020/api/v1/Product", createProduct);
var idProduct = await responseGetProduct.Content.ReadFromJsonAsync<int>();

responseGetProduct = await new HttpClient().GetAsync("https://localhost:7020/api/v1/Product");
products = await responseGetProduct.Content.ReadFromJsonAsync<List<AllProduct>>();
foreach (var product in products)
{
    Console.WriteLine($"Id: {product.Id} Name: {product.Name} Description: {product.Description}");
}

//Update-PUT
Console.WriteLine("Enter Id of Product for Update");
int id_pput = int.Parse(Console.ReadLine());
Console.WriteLine("Enter Name of Product for Update");
string name_pput = Console.ReadLine();
Console.WriteLine("Enter description of Product for Update");
string description_pput = Console.ReadLine();
Console.WriteLine("Enter Pricte of Product for Add");
decimal price_pput = decimal.Parse(Console.ReadLine());
Console.WriteLine("Enter IdProductCategory of Product for Add");
int id_pcput = int.Parse(Console.ReadLine());
var updatedProduct = new UpdateProduct
{
    Id = id_pput,
    Name = name_pput,
    Description = description_pput,
    Price = price_pput,
    IdProductCategory = id_pcput
};
responseGetProduct = await new HttpClient().PutAsJsonAsync("https://localhost:7020/api/v1/Product", updatedProduct);


responseGetProduct = await new HttpClient().GetAsync("https://localhost:7020/api/v1/Product");
products = await responseGetProduct.Content.ReadFromJsonAsync<List<AllProduct>>();
foreach (var product in products)
{
    Console.WriteLine($"Id: {product.Id} Name: {product.Name} Description: {product.Description} Price: {product.Price}");
}


Console.ReadLine();
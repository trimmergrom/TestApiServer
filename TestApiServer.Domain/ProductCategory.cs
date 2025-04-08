﻿namespace TestApiServer.Domain
{
    public class ProductCategory
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}

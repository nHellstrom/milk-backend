using System;
using Microsoft.AspNetCore.Mvc;
using milk_backend.Services;
using milk_backend.Models;

namespace milk_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase {
    private readonly MongoDBService _mongoDBService;

    public ProductController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;    
    }

    [HttpGet]
    public async Task<List<Product>> Get() 
    {
        return await _mongoDBService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<ProductDTO> GetByLegacyId(string id) 
    {
        return await _mongoDBService.GetByLegacyId(id);
    }

    [HttpPatch("Order/{id}")]
    public async Task<ProductDTO> UpdateProductQuantity(string id, [FromBody] int amount) 
    {
        Console.WriteLine("Amount from the client " + amount);
        return await _mongoDBService.UpdateQuantityAsync(id, amount);
    }
}
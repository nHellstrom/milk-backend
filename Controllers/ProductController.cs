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

  
    // [HttpPost]
    // public async Task<IActionResult> Post([FromBody] Product Product) 
    // {
    //     await _mongoDBService.CreateAsync(Product);
    //     return CreatedAtAction(nameof(Get), new { id = Product._id}, Product);
    // }

    [HttpPatch("Order/{id}")]
    public async Task<ProductDTO> UpdateProductQuantity(string id, [FromBody] int amount) 
    {
        Console.WriteLine("Amount from the client " + amount);
        return await _mongoDBService.UpdateQuantityAsync(id, amount);
        // return NoContent();
    }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id) 
    // {
    //     await _mongoDBService.DeleteAsync(id);
    //     return NoContent();
    // }
}
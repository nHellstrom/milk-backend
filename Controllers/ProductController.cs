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

    [HttpGet("{name}")]
    public async Task<List<Product>> GetByName(string name) 
    {
        return await _mongoDBService.GetByName();
    }
  
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product Product) 
    {
        await _mongoDBService.CreateAsync(Product);
        return CreatedAtAction(nameof(Get), new { id = Product._id}, Product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product) 
    {
        await _mongoDBService.UpdateAsync(id, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) 
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}
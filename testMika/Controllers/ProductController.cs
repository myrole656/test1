using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testMika.DAL.Data;
using testMika.DAL.Entities;
using testMika.Models;

namespace testMika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() =>
            await _db.Products.AsNoTracking().ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            return product is null ? NotFound() : product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductDto dto)
        {
            var product = await _db.Products.FindAsync(id);
            if (product is null) return NotFound();

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product is null) return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

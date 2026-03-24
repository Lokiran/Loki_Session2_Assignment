using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ProductInventoryManagement.Data;
using ProductInventoryManagement.Models;

namespace ProductInventoryManagement.Controllers
{
    [Authorize]  // Token required for ALL actions
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _db;

        public ProductsController(ProductDbContext context)
        {
            _db = context;
        }

        // GET: api/products
        // Viewer + Manager + Admin
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet]
        public async Task<ActionResult<object>> GetAllProducts()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var products = await _db.Products.ToListAsync();

            return Ok(new
            {
                calledBy = username,
                callerRole = role,
                data = products
            });
        }

        // GET: api/products/5
        // Viewer + Manager + Admin
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProductById(int id)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(new
            {
                calledBy = username,
                callerRole = role,
                data = product
            });
        }

        // POST: api/products
        // Admin + Manager
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (product.Price <= 0)
                return BadRequest("Price should be greater than 0");

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        // Admin + Manager
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Id mismatch");

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch
            {
                if (!_db.Products.Any(p => p.Id == id))
                    return NotFound("Product not found");

                throw;
            }

            return NoContent();
        }

        // DELETE: api/products/5
        // Admin ONLY
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // GET CATEGORY
        // Viewer + Manager + Admin
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("category/{category}")]
        public async Task<ActionResult<object>> GetByCategory(string category)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var products = await _db.Products
                .Where(p => p.Category == category)
                .ToListAsync();

            return Ok(new
            {
                calledBy = username,
                callerRole = role,
                data = products
            });
        }

        // GET SORTED
        // Viewer + Manager + Admin
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("sorted")]
        public async Task<ActionResult<object>> GetSorted()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var products = await _db.Products
                .OrderBy(p => p.Price)
                .ToListAsync();

            return Ok(new
            {
                calledBy = username,
                callerRole = role,
                data = products
            });
        }

        // GET OUT OF STOCK
        // Viewer + Manager + Admin
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("outofstock")]
        public async Task<ActionResult<object>> GetOutOfStock()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var products = await _db.Products
                .Where(p => p.StockQuantity == 0)
                .ToListAsync();

            return Ok(new
            {
                calledBy = username,
                callerRole = role,
                data = products
            });
        }
    }
}
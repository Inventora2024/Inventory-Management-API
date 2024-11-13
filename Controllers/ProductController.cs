using AutoMapper;
using Inventory_Management_API.DTOs;
using Inventory_Management_API.Models;
using Inventory_Management_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public ProductController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        var productDTOs = _mapper.Map<List<ProductDTO>>(products);
        return productDTOs;
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var productDTO = _mapper.Map<ProductDTO>(product);
        return productDTO;
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
    {
        if (id != productDTO.ProductId)
        {
            return BadRequest();
        }

        var product = _mapper.Map<Product>(productDTO);
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, productDTO);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("with-categories")]
    public async Task<ActionResult<IEnumerable<ProductWithCategoryDTO>>> GetProductsWithCategories()
    {
        var productsWithCategories = await _context.Products
            .Join(_context.ProductCategories,
                product => product.CategoryId,
                category => category.CategoryId,
                (product, category) => new ProductWithCategoryDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Image = product.Image,
                    StockQuantity = product.StockQuantity,
                    CategoryId = product.CategoryId,
                    Category = category.Category,
                    Nature = category.Nature
                })
            .ToListAsync();

        return Ok(productsWithCategories);
    }

    [HttpGet("with-categories-suppliers")]
    public async Task<ActionResult<IEnumerable<ProductCategorySupplierDetailsDTO>>> GetProductsWithDetails()
    {
        var productsWithDetails = await _context.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.SupplierProducts)
                .ThenInclude(sp => sp.Supplier)
            .Select(product => new ProductCategorySupplierDetailsDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Image = product.Image,
                Category = product.ProductCategory.Category,
                Nature = product.ProductCategory.Nature,
                Suppliers = product.SupplierProducts.Select(sp => sp.Supplier.Company).ToList()
            })
            .ToListAsync();

        return Ok(productsWithDetails);
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
}

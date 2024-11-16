using AutoMapper;
using Inventory_Management_API.DTOs;
using Inventory_Management_API.Models;
using Inventory_Management_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductCategoryController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public ProductCategoryController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/ProductCategory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
    {
        var productCategories = await _context.ProductCategories.ToListAsync();
        var productCategoryDTOs = _mapper.Map<List<ProductCategoryDTO>>(productCategories);
        return productCategoryDTOs;
    }

    // GET: api/ProductCategory/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategoryDTO>> GetProductCategory(int id)
    {
        var productCategory = await _context.ProductCategories.FindAsync(id);

        if (productCategory == null)
        {
            return NotFound();
        }

        var productCategoryDTO = _mapper.Map<ProductCategoryDTO>(productCategory);
        return productCategoryDTO;
    }

    // PUT: api/ProductCategory/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductCategory(int id, ProductCategoryDTO productCategoryDTO)
    {
        if (id != productCategoryDTO.CategoryId)
        {
            return BadRequest();
        }

        var productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
        _context.Entry(productCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductCategoryExists(id))
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

    // POST: api/ProductCategory
    [HttpPost]
    public async Task<ActionResult<ProductCategoryDTO>> PostProductCategory(ProductCategoryDTO productCategoryDTO)
    {
        var productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
        _context.ProductCategories.Add(productCategory);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductCategory), new { id = productCategory.CategoryId }, productCategoryDTO);
    }

    // DELETE: api/ProductCategory/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductCategory(int id)
    {
        var productCategory = await _context.ProductCategories.FindAsync(id);
        if (productCategory == null)
        {
            return NotFound();
        }

        _context.ProductCategories.Remove(productCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductCategoryExists(int id)
    {
        return _context.ProductCategories.Any(e => e.CategoryId == id);
    }
}

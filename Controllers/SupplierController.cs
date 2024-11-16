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
public class SupplierController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public SupplierController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Supplier
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
    {
        var suppliers = await _context.Suppliers.ToListAsync();
        var supplierDTOs = _mapper.Map<List<SupplierDTO>>(suppliers);
        return supplierDTOs;
    }

    // GET: api/Supplier/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDTO>> GetSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);

        if (supplier == null)
        {
            return NotFound();
        }

        var supplierDTO = _mapper.Map<SupplierDTO>(supplier);
        return supplierDTO;
    }

    // PUT: api/Supplier/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSupplier(int id, SupplierDTO supplierDTO)
    {
        if (id != supplierDTO.SupplierId)
        {
            return BadRequest();
        }

        var supplier = _mapper.Map<Supplier>(supplierDTO);
        _context.Entry(supplier).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SupplierExists(id))
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

    // POST: api/Supplier
    [HttpPost]
    public async Task<ActionResult<SupplierDTO>> PostSupplier(SupplierDTO supplierDTO)
    {
        var supplier = _mapper.Map<Supplier>(supplierDTO);
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSupplier), new { id = supplier.SupplierId }, supplierDTO);
    }

    // DELETE: api/Supplier/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound();
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SupplierExists(int id)
    {
        return _context.Suppliers.Any(e => e.SupplierId == id);
    }
}

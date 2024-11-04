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
public class StockOrderItemController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public StockOrderItemController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/StockOrderItem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockOrderItemDTO>>> GetStockOrderItems()
    {
        var stockOrderItems = await _context.StockOrderItems.ToListAsync();
        var stockOrderItemDTOs = _mapper.Map<List<StockOrderItemDTO>>(stockOrderItems);
        return stockOrderItemDTOs;
    }

    // GET: api/StockOrderItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StockOrderItemDTO>> GetStockOrderItem(int id)
    {
        var stockOrderItem = await _context.StockOrderItems.FindAsync(id);

        if (stockOrderItem == null)
        {
            return NotFound();
        }

        var stockOrderItemDTO = _mapper.Map<StockOrderItemDTO>(stockOrderItem);
        return stockOrderItemDTO;
    }

    // PUT: api/StockOrderItem/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStockOrderItem(int id, StockOrderItemDTO stockOrderItemDTO)
    {
        if (id != stockOrderItemDTO.StockOrderItemId)
        {
            return BadRequest();
        }

        var stockOrderItem = _mapper.Map<StockOrderItem>(stockOrderItemDTO);
        _context.Entry(stockOrderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StockOrderItemExists(id))
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

    // POST: api/StockOrderItem
    [HttpPost]
    public async Task<ActionResult<StockOrderItemDTO>> PostStockOrderItem(StockOrderItemDTO stockOrderItemDTO)
    {
        var stockOrderItem = _mapper.Map<StockOrderItem>(stockOrderItemDTO);
        _context.StockOrderItems.Add(stockOrderItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStockOrderItem), new { id = stockOrderItem.StockOrderItemId }, stockOrderItemDTO);
    }

    // DELETE: api/StockOrderItem/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockOrderItem(int id)
    {
        var stockOrderItem = await _context.StockOrderItems.FindAsync(id);
        if (stockOrderItem == null)
        {
            return NotFound();
        }

        _context.StockOrderItems.Remove(stockOrderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StockOrderItemExists(int id)
    {
        return _context.StockOrderItems.Any(e => e.StockOrderItemId == id);
    }
}

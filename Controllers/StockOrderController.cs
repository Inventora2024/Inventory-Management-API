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
public class StockOrderController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public StockOrderController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/StockOrder
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockOrderDTO>>> GetStockOrders()
    {
        var stockOrders = await _context.StockOrders.ToListAsync();
        var stockOrderDTOs = _mapper.Map<List<StockOrderDTO>>(stockOrders);
        return stockOrderDTOs;
    }

    // GET: api/StockOrder/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StockOrderDTO>> GetStockOrder(int id)
    {
        var stockOrder = await _context.StockOrders.FindAsync(id);

        if (stockOrder == null)
        {
            return NotFound();
        }

        var stockOrderDTO = _mapper.Map<StockOrderDTO>(stockOrder);
        return stockOrderDTO;
    }

    // PUT: api/StockOrder/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStockOrder(int id, StockOrderDTO stockOrderDTO)
    {
        if (id != stockOrderDTO.StockOrderId)
        {
            return BadRequest();
        }

        var stockOrder = _mapper.Map<StockOrder>(stockOrderDTO);
        _context.Entry(stockOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StockOrderExists(id))
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

    // POST: api/StockOrder
    [HttpPost]
    public async Task<ActionResult<StockOrderDTO>> PostStockOrder(StockOrderDTO stockOrderDTO)
    {
        var stockOrder = _mapper.Map<StockOrder>(stockOrderDTO);
        _context.StockOrders.Add(stockOrder);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStockOrder), new { id = stockOrder.StockOrderId }, stockOrderDTO);
    }

    // DELETE: api/StockOrder/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockOrder(int id)
    {
        var stockOrder = await _context.StockOrders.FindAsync(id);
        if (stockOrder == null)
        {
            return NotFound();
        }

        _context.StockOrders.Remove(stockOrder);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StockOrderExists(int id)
    {
        return _context.StockOrders.Any(e => e.StockOrderId == id);
    }
}

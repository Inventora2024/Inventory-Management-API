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

    [HttpGet("Products")]
    public async Task<ActionResult<IEnumerable<StockOrderProductsDTO>>> GetStockOrderProducts()
    {
        var stockOrders = await _context.StockOrders
            .Include(so => so.StockOrderItems)
            .ThenInclude(soi => soi.Product)
            .ToListAsync();

        var stockOrderProductsDTOs = _mapper.Map<List<StockOrderProductsDTO>>(stockOrders);
        return stockOrderProductsDTOs;
    }

    [HttpPost("CreateOrder")]
    public async Task<ActionResult<StockOrderDTO>> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var stockOrder = new StockOrder
        {
            OrderDate = createOrderDTO.OrderDate,
            StockOrderItems = createOrderDTO.OrderItems.Select(item => new StockOrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };

        _context.StockOrders.Add(stockOrder);
        await _context.SaveChangesAsync();

        var shipment = new Shipment
        {
            LastUpdated = DateTime.UtcNow,
            Status = "Order Confirmed",
            StockOrderId = stockOrder.StockOrderId
        };

        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStockOrder), new { id = stockOrder.StockOrderId }, _mapper.Map<StockOrderDTO>(stockOrder));
    }

    [HttpGet("OrdersWithShipments")]
    public async Task<ActionResult<IEnumerable<StockOrderShipmentDTO>>> GetStockOrdersWithShipments()
    {
        var stockOrdersWithShipments = await _context.StockOrders
            .Include(so => so.Shipment)
            .ToListAsync();

        var stockOrderShipmentDTOs = stockOrdersWithShipments.Select(so => new StockOrderShipmentDTO
        {
            ShipmentId = so.Shipment.ShipmentId,
            StockOrderId = so.StockOrderId,
            OrderId = so.StockOrderId,  // Assuming OrderId is same as StockOrderId in your model
            OrderDate = so.OrderDate,
            Status = so.Shipment.Status,
            LastUpdated = so.Shipment.LastUpdated
        }).ToList();

        return stockOrderShipmentDTOs;
    }

    private bool StockOrderExists(int id)
    {
        return _context.StockOrders.Any(e => e.StockOrderId == id);
    }
}

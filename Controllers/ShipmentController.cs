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
public class ShipmentController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public ShipmentController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Shipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetShipments()
    {
        var shipments = await _context.Shipments.ToListAsync();
        var shipmentDTOs = _mapper.Map<List<ShipmentDTO>>(shipments);
        return shipmentDTOs;
    }

    // GET: api/Shipment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShipmentDTO>> GetShipment(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);

        if (shipment == null)
        {
            return NotFound();
        }

        var shipmentDTO = _mapper.Map<ShipmentDTO>(shipment);
        return shipmentDTO;
    }

    // PUT: api/Shipment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShipment(int id, ShipmentDTO shipmentDTO)
    {
        if (id != shipmentDTO.ShipmentId)
        {
            return BadRequest();
        }

        var shipment = _mapper.Map<Shipment>(shipmentDTO);
        _context.Entry(shipment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShipmentExists(id))
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

    // POST: api/Shipment
    [HttpPost]
    public async Task<ActionResult<ShipmentDTO>> PostShipment(ShipmentDTO shipmentDTO)
    {
        var shipment = _mapper.Map<Shipment>(shipmentDTO);
        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShipment), new { id = shipment.ShipmentId }, shipmentDTO);
    }

    // DELETE: api/Shipment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipment(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShipmentExists(int id)
    {
        return _context.Shipments.Any(e => e.ShipmentId == id);
    }
}

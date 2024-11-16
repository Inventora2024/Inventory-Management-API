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
public class CustomerOrderItemController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public CustomerOrderItemController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/CustomerOrderItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerOrderItemDTO>>> GetCustomerOrderItems()
    {
        var customerOrderItems = await _context.CustomerOrderItems.ToListAsync();
        var customerOrderItemDTOs = _mapper.Map<List<CustomerOrderItemDTO>>(customerOrderItems);
        return customerOrderItemDTOs;
    }

    // GET: api/CustomerOrderItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerOrderItemDTO>> GetCustomerOrderItem(int id)
    {
        var customerOrderItem = await _context.CustomerOrderItems.FindAsync(id);

        if (customerOrderItem == null)
        {
            return NotFound();
        }

        var customerOrderItemDTO = _mapper.Map<CustomerOrderItemDTO>(customerOrderItem);
        return customerOrderItemDTO;
    }

    // PUT: api/CustomerOrderItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomerOrderItem(int id, CustomerOrderItemDTO customerOrderItemDTO)
    {
        if (id != customerOrderItemDTO.CustomerOrderItemId)
        {
            return BadRequest();
        }

        var customerOrderItem = _mapper.Map<CustomerOrderItem>(customerOrderItemDTO);
        _context.Entry(customerOrderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerOrderItemExists(id))
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

    // POST: api/CustomerOrderItems
    [HttpPost]
    public async Task<ActionResult<CustomerOrderItemDTO>> PostCustomerOrderItem(CustomerOrderItemDTO customerOrderItemDTO)
    {
        var customerOrderItem = _mapper.Map<CustomerOrderItem>(customerOrderItemDTO);
        _context.CustomerOrderItems.Add(customerOrderItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomerOrderItem), new { id = customerOrderItem.CustomerOrderItemId }, customerOrderItemDTO);
    }

    // DELETE: api/CustomerOrderItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerOrderItem(int id)
    {
        var customerOrderItem = await _context.CustomerOrderItems.FindAsync(id);
        if (customerOrderItem == null)
        {
            return NotFound();
        }

        _context.CustomerOrderItems.Remove(customerOrderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CustomerOrderItemExists(int id)
    {
        return _context.CustomerOrderItems.Any(e => e.CustomerOrderItemId == id);
    }
}

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
public class CustomerOrderController : ControllerBase
{
    private readonly InventoryManagementDbContext _context;
    private readonly IMapper _mapper;

    public CustomerOrderController(InventoryManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/CustomerOrders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerOrderDTO>>> GetCustomerOrders()
    {
        var customerOrders = await _context.CustomerOrders.ToListAsync();
        var customerOrderDTOs = _mapper.Map<List<CustomerOrderDTO>>(customerOrders);
        return customerOrderDTOs;
    }

    // GET: api/CustomerOrders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerOrderDTO>> GetCustomerOrder(int id)
    {
        var customerOrder = await _context.CustomerOrders.FindAsync(id);

        if (customerOrder == null)
        {
            return NotFound();
        }

        var customerOrderDTO = _mapper.Map<CustomerOrderDTO>(customerOrder);
        return customerOrderDTO;
    }

    // PUT: api/CustomerOrders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomerOrder(int id, CustomerOrderDTO customerOrderDTO)
    {
        if (id != customerOrderDTO.CustomerOrderId)
        {
            return BadRequest();
        }

        var customerOrder = _mapper.Map<CustomerOrder>(customerOrderDTO);
        _context.Entry(customerOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerOrderExists(id))
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

    // POST: api/CustomerOrders
    [HttpPost]
    public async Task<ActionResult<CustomerOrderDTO>> PostCustomerOrder(CustomerOrderDTO customerOrderDTO)
    {
        var customerOrder = _mapper.Map<CustomerOrder>(customerOrderDTO);
        _context.CustomerOrders.Add(customerOrder);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomerOrder), new { id = customerOrder.CustomerOrderId }, customerOrderDTO);
    }

    // DELETE: api/CustomerOrders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerOrder(int id)
    {
        var customerOrder = await _context.CustomerOrders.FindAsync(id);
        if (customerOrder == null)
        {
            return NotFound();
        }

        _context.CustomerOrders.Remove(customerOrder);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CustomerOrderExists(int id)
    {
        return _context.CustomerOrders.Any(e => e.CustomerOrderId == id);
    }
}

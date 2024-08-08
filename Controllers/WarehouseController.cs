using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketInventory.Data;
using SupermarketInventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly SupermarketContext _context;

        public WarehouseController(SupermarketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Warehouse>>> Get()
        {
            return await _context.Warehouses.Include(w => w.Items).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> Get(int id)
        {
            var warehouse = await _context.Warehouses.Include(w => w.Items).FirstOrDefaultAsync(w => w.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return warehouse;
        }

        [HttpPost]
        public async Task<ActionResult<Warehouse>> Post(Warehouse warehouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = warehouse.WarehouseId }, warehouse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Warehouse warehouse)
        {
            if (id != warehouse.WarehouseId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(warehouse).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(e => e.WarehouseId == id);
        }
    }
}

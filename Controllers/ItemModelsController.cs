using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RankingAPI.Models;

namespace RankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Marca la clase con el atributo [ApiController]. Este atributo indica que el controlador responde a las solicitudes de la API web. 
    public class ItemModelsController : ControllerBase
    {
        private readonly ItemContext _context; //Utiliza la inserción de dependencias para insertar el contexto de base de datos (TodoContext) en el controlador. El contexto de base de datos se usa en cada uno de los métodos CRUD del controlador.

        public ItemModelsController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/ItemModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetItemItems()
        {
            if (_context.ItemItems == null)
            {
                return NotFound();
            }
            return await _context.ItemItems.ToListAsync();
        }

        // GET: api/ItemModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> GetItemModel(int id)
        {
            if (_context.ItemItems == null)
            {
                return NotFound();
            }
            var itemModel = await _context.ItemItems.FindAsync(id);

            if (itemModel == null)
            {
                return NotFound();
            }

            return itemModel;
        }

        // PUT: api/ItemModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemModel(int id, ItemModel itemModel)
        {
            if (id != itemModel.id)
            {
                return BadRequest();
            }

            _context.Entry(itemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemModelExists(id))
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

        // POST: api/ItemModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemModel>> PostItemModel(ItemModel itemModel)
        {
            if (_context.ItemItems == null)
            {
                return Problem("Entity set 'ItemContext.TodoItems'  is null.");
            }
            _context.ItemItems.Add(itemModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemModel), new { id = itemModel.id }, itemModel);
        }

        // DELETE: api/ItemModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemModel(int id)
        {
            if (_context.ItemItems == null)
            {
                return NotFound();
            }
            var itemModel = await _context.ItemItems.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            _context.ItemItems.Remove(itemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemModelExists(int id)
        {
            return (_context.ItemItems?.Any(e => e.id == id)).GetValueOrDefault();
        }

    
        [HttpGet("ResetType/{id}")]
        public async Task<IActionResult> CleanType(int id)
        {
            var items = _context.ItemItems.Where(t => t.itemType == id);
            foreach(ItemModel itm in items)
            {
                itm.ranking = 0;
                itm.tierId = 0;
                _context.Entry(itm).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemModelExists(id))
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

        [HttpPatch("{id}/tierId/{tierId}/{ranking}")]
        public async Task<IActionResult> PatchTierModel(int id, int tierId, int ranking)
        {
            var item = _context.ItemItems.Single(t => t.id == id);
            item.tierId = tierId;
            item.ranking = ranking;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemModelExists(id))
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


    }
}

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
    [ApiController]
    public class TierModelsController : ControllerBase
    {
        private readonly TierContext _context;
        private readonly ItemContext _contextItem;
        public TierModelsController(TierContext context, ItemContext contextItem)
        {
            _context = context;
            _contextItem = contextItem;
        }



        // GET: api/TierModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TierModel>>> GetTierModel()
        {
            if (_context.Tiers == null)
            {
                return NotFound();
            }
            return await _context.Tiers.ToListAsync();
        }


        // GET: api/TierModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TierModel>> GetTierModel(int id)
        {
          if (_context.Tiers == null)
          {
              return NotFound();
          }
            var tierModel = await _context.Tiers.FindAsync(id);

            if (tierModel == null)
            {
                return NotFound();
            }

            return tierModel;
        }

        // PUT: api/TierModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTierModel(int id, TierModel tierModel)
        {
            if (id != tierModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(tierModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TierModelExists(id))
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


        // PATCH: api/TierModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTierModel(int id, TierModel tierModel)
        {
            if (id != tierModel.Id)
            {
                return BadRequest();
            }

            var tier = _context.Tiers.Single(t => t.Id == id);
            tier.NumCells = tierModel.NumCells;
            tier.RowName = tierModel.RowName;
            RemoveExcess(id, tierModel.NumCells);
           _context.Entry(tier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TierModelExists(id))
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

        // POST: api/TierModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TierModel>> PostTierModel(TierModel tierModel)
        {
          if (_context.Tiers == null)
          {
              return Problem("Entity set 'TierContext.TierModel' is null.");
          }

            if (tierModel.Id < 1 )
            {
                tierModel.Id = _context.Tiers.Count() > 0 ? _context.Tiers.Max(p => p.Id) + 1 : 1;
            }


            if (tierModel.RowNumber == 0)
            {
                tierModel.RowNumber = _context.Tiers.Count() > 0 ? _context.Tiers.Max(p => p.RowNumber) + 1 : 1;
            }
            _context.Tiers.Add(tierModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTierModel", new { id = tierModel.Id }, tierModel);
        }

        // DELETE: api/TierModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTierModel(int id)
        {
            if (_context.Tiers == null)
            {
                return NotFound();
            }
            var tierModel = await _context.Tiers.FindAsync(id);
            if (tierModel == null)
            {
                return NotFound();
            }
            EmptyTier(id);
            _context.Tiers.Remove(tierModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TierModelExists(int id)
        {
            return (_context.Tiers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        protected void RemoveExcess(int tierId, int cantCells) 
        {
            var items = _contextItem.ItemItems.Where(t => t.tierId == tierId);
            foreach (ItemModel item in items)
            {
                if(item.ranking > cantCells)
                {
                    item.tierId = 0;
                    item.ranking = 0;
                }
                _contextItem.Entry(item).State = EntityState.Modified;
            }
            _contextItem.SaveChangesAsync();
        }

        protected void EmptyTier(int tierid)
        {
            var items = _contextItem.ItemItems.Where(t => t.tierId == tierid);
            foreach (ItemModel item in items)
            {
                item.tierId = 0;
                item.ranking = 0;
                _contextItem.Entry(item).State = EntityState.Modified;
            }
            _contextItem.SaveChangesAsync();
        }
    }
}

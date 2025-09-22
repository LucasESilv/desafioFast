using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rastreamentoWorkshopAPI.Data;
using rastreamentoWorkshopAPI.DTOs.WorkshopDTO;

namespace rastreamentoWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkshopsController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkshopReadDTO>>> GetWorkshops()
        {
            if (_context.Workshops == null)
                return NotFound();

            var workshops = await _context.Workshops.ToListAsync();
            var dtos = workshops.Select(w => new WorkshopReadDTO
            {
                Id = w.Id,
                Nome = w.Nome,
                DataRealizacao = w.DataRealizacao,
                Descricao = w.Descricao
            }).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkshopReadDTO>> GetWorkshop(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null) return NotFound();
            var readDto = new WorkshopReadDTO
            {
                Id = workshop.Id,
                Nome = workshop.Nome,
                DataRealizacao = workshop.DataRealizacao,
                Descricao = workshop.Descricao
            };
            return Ok(readDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshop(int id, Workshop workshop)
        {
            if (id != workshop.Id)
            {
                return BadRequest();
            }

            _context.Entry(workshop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopExists(id))
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

        [HttpPost]
        public async Task<ActionResult<WorkshopReadDTO>> PostWorkshop(WorkshopCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var workshop = new Workshop
            {
                Nome = dto.Nome,
                DataRealizacao = dto.DataRealizacao,
                Descricao = dto.Descricao
            };
            _context.Workshops.Add(workshop);
            await _context.SaveChangesAsync();
            var readDto = new WorkshopReadDTO
            {
                Id = workshop.Id,
                Nome = workshop.Nome,
                DataRealizacao = workshop.DataRealizacao,
                Descricao = workshop.Descricao
            };
            return CreatedAtAction(nameof(GetWorkshop), new { id = workshop.Id }, readDto);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshop(int id)
        {
            if (_context.Workshops == null)
            {
                return NotFound();
            }
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null)
            {
                return NotFound();
            }

            _context.Workshops.Remove(workshop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkshopExists(int id)
        {
            return (_context.Workshops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

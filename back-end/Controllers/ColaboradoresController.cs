using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rastreamentoWorkshopAPI.Data;
using rastreamentoWorkshopAPI.DTOs.ColaboradorDTO;
using rastreamentoWorkshopAPI.DTOs.WorkshopDTO;

namespace rastreamentoWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Colaboradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColaboradorReadDTO>>> GetColaboradores()
        {
            if (_context.Colaboradores == null)
                return NotFound();

            var colaboradores = await _context.Colaboradores.ToListAsync();
            var dtos = colaboradores.Select(c => new ColaboradorReadDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();

            return Ok(dtos);
        }

        [HttpGet("com-workshops")]
        public async Task<ActionResult<IEnumerable<ColaboradorComWorkshopsReadDTO>>> GetColaboradoresComWorkshops()
        {
            var colaboradores = await _context.Colaboradores
                .Include(c => c.AtaColaboradores)
                .ThenInclude(ac => ac.AtaWorkshop)
                .ThenInclude(aw => aw.Workshop)
                .OrderBy(c => c.Nome)
                .ToListAsync();

            var result = colaboradores.Select(c => new ColaboradorComWorkshopsReadDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Workshops = c.AtaColaboradores
                    .Select(ac => ac.AtaWorkshop.Workshop)
                    .Distinct()
                    .Select(w => new WorkshopReadDTO
                    {
                        Id = w.Id,
                        Nome = w.Nome,
                        DataRealizacao = w.DataRealizacao,
                        Descricao = w.Descricao
                    })
                    .ToList()
            }).ToList();

            return Ok(result);
        }


        // GET: api/Colaboradores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColaboradorReadDTO>> GetColaborador(int id)
        {
            if (_context.Colaboradores == null)
                return NotFound();

            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null)
                return NotFound();

            var dto = new ColaboradorReadDTO
            {
                Id = colaborador.Id,
                Nome = colaborador.Nome
            };

            return Ok(dto);
        }

        // POST: api/Colaboradores
        [HttpPost]
        public async Task<ActionResult<ColaboradorReadDTO>> PostColaborador(ColaboradorCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var colaborador = new Colaborador
            {
                Nome = dto.Nome
            };

            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();

            var readDto = new ColaboradorReadDTO
            {
                Id = colaborador.Id,
                Nome = colaborador.Nome
            };

            return CreatedAtAction(nameof(GetColaborador), new { id = colaborador.Id }, readDto);
        }

        // PUT: api/Colaboradores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, ColaboradorCreateDTO dto)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null)
                return NotFound();

            colaborador.Nome = dto.Nome;
            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Colaboradores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradores == null)
                return NotFound();

            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null)
                return NotFound();

            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

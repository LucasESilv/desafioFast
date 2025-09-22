using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rastreamentoWorkshopAPI.Data;
using rastreamentoWorkshopAPI.DTOs.AtaWorkshopDTO;
using rastreamentoWorkshopAPI.DTOs.ColaboradorDTO;
using rastreamentoWorkshopAPI.DTOs.WorkshopDTO;

namespace rastreamentoWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtaWorkshopsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AtaWorkshopsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AtaWorkshops?workshopNome={nome}&data={data}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AtaWorkshopReadDTO>>> GetAtas(
            [FromQuery] string? workshopNome, [FromQuery] DateTime? data)
        {
            if (_context.Atas == null)
                return NotFound();

            var query = _context.Atas
                .Include(a => a.Workshop)
                .Include(a => a.AtaColaboradores)
                    .ThenInclude(ac => ac.Colaborador)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(workshopNome))
                query = query.Where(a => a.Workshop.Nome.Contains(workshopNome));

            if (data.HasValue)
                query = query.Where(a => a.Workshop.DataRealizacao.Date == data.Value.Date);

            var atas = await query.ToListAsync();

            var dtos = atas.Select(a => new AtaWorkshopReadDTO
            {
                Id = a.Id,
                Workshop = new WorkshopReadDTO
                {
                    Id = a.Workshop.Id,
                    Nome = a.Workshop.Nome,
                    DataRealizacao = a.Workshop.DataRealizacao,
                    Descricao = a.Workshop.Descricao
                },
                Colaboradores = a.AtaColaboradores.Select(ac => new ColaboradorReadDTO
                {
                    Id = ac.Colaborador.Id,
                    Nome = ac.Colaborador.Nome
                }).ToList()
            }).ToList();

            return Ok(dtos);
        }

        // GET: api/AtaWorkshops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AtaWorkshopReadDTO>> GetAtaWorkshop(int id)
        {
            if (_context.Atas == null)
                return NotFound();

            var ata = await _context.Atas
                .Include(a => a.Workshop)
                .Include(a => a.AtaColaboradores)
                    .ThenInclude(ac => ac.Colaborador)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ata == null)
                return NotFound();

            var dto = new AtaWorkshopReadDTO
            {
                Id = ata.Id,
                Workshop = new WorkshopReadDTO
                {
                    Id = ata.Workshop.Id,
                    Nome = ata.Workshop.Nome,
                    DataRealizacao = ata.Workshop.DataRealizacao,
                    Descricao = ata.Workshop.Descricao
                },
                Colaboradores = ata.AtaColaboradores.Select(ac => new ColaboradorReadDTO
                {
                    Id = ac.Colaborador.Id,
                    Nome = ac.Colaborador.Nome
                }).ToList()
            };

            return Ok(dto);
        }

        // POST: api/AtaWorkshops
        [HttpPost]
        public async Task<ActionResult<AtaWorkshopReadDTO>> PostAtaWorkshop(AtaWorkshopCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var workshop = await _context.Workshops.FindAsync(dto.WorkshopId);
            if (workshop == null)
                return BadRequest("Workshop não encontrado");

            var colaboradores = await _context.Colaboradores
                .Where(c => dto.ColaboradorIds.Contains(c.Id))
                .ToListAsync();

            var ataColaboradores = colaboradores.Select(colab => new AtaColaborador
            {
                ColaboradorId = colab.Id
            }).ToList();

            var ata = new AtaWorkshop
            {
                WorkshopId = dto.WorkshopId,
                AtaColaboradores = ataColaboradores
            };

            _context.Atas.Add(ata);
            await _context.SaveChangesAsync();

            var readDto = new AtaWorkshopReadDTO
            {
                Id = ata.Id,
                Workshop = new WorkshopReadDTO
                {
                    Id = workshop.Id,
                    Nome = workshop.Nome,
                    DataRealizacao = workshop.DataRealizacao,
                    Descricao = workshop.Descricao
                },
                Colaboradores = colaboradores.Select(c => new ColaboradorReadDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).ToList()
            };

            return CreatedAtAction(nameof(GetAtaWorkshop), new { id = ata.Id }, readDto);
        }

        // PUT: api/AtaWorkshops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtaWorkshop(int id, AtaWorkshopCreateDTO dto)
        {
            var ata = await _context.Atas
                .Include(a => a.AtaColaboradores)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ata == null)
                return NotFound();

            ata.WorkshopId = dto.WorkshopId;
            ata.AtaColaboradores.Clear();

            var novosAtaColaboradores = dto.ColaboradorIds.Select(colabId =>
                new AtaColaborador { AtaWorkshopId = ata.Id, ColaboradorId = colabId }).ToList();

            ata.AtaColaboradores.AddRange(novosAtaColaboradores);

            _context.Entry(ata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtaWorkshopExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/AtaWorkshops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtaWorkshop(int id)
        {
            if (_context.Atas == null)
                return NotFound();

            var ata = await _context.Atas.FindAsync(id);
            if (ata == null)
                return NotFound();

            _context.Atas.Remove(ata);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{ataId}/colaboradores/{colaboradorId}")]
        public async Task<IActionResult> RemoverColaboradorDaAta(int ataId, int colaboradorId)
        {
            var ata = await _context.Atas
                .Include(a => a.AtaColaboradores)
                .FirstOrDefaultAsync(a => a.Id == ataId);

            if (ata == null)
                return NotFound("Ata não encontrada.");

            var ataColaborador = ata.AtaColaboradores
                .FirstOrDefault(ac => ac.ColaboradorId == colaboradorId);

            if (ataColaborador == null)
                return NotFound("Colaborador não encontrado nesta ata.");

            ata.AtaColaboradores.Remove(ataColaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AtaWorkshopExists(int id)
        {
            return _context.Atas.Any(e => e.Id == id);
        }
    }
}

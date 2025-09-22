using rastreamentoWorkshopAPI.DTOs.WorkshopDTO;

namespace rastreamentoWorkshopAPI.DTOs.ColaboradorDTO;

public class ColaboradorComWorkshopsReadDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<WorkshopReadDTO> Workshops { get; set; }
}
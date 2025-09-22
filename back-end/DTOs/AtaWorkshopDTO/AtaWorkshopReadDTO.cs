using rastreamentoWorkshopAPI.DTOs.ColaboradorDTO;
using rastreamentoWorkshopAPI.DTOs.WorkshopDTO;

namespace rastreamentoWorkshopAPI.DTOs.AtaWorkshopDTO;

public class AtaWorkshopReadDTO
{
    public int Id { get; set; }
    public WorkshopReadDTO Workshop { get; set; }
    public List<ColaboradorReadDTO> Colaboradores { get; set; }
}
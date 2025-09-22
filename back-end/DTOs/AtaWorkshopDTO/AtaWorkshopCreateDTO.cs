namespace rastreamentoWorkshopAPI.DTOs.AtaWorkshopDTO;

public class AtaWorkshopCreateDTO
{
    public int WorkshopId { get; set; }
    public List<int> ColaboradorIds { get; set; } = new List<int>();
}
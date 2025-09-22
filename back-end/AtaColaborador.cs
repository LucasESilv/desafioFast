namespace rastreamentoWorkshopAPI;

public class AtaColaborador
{
    public int AtaWorkshopId { get; set; }
    public AtaWorkshop AtaWorkshop { get; set; }
    
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; }
}
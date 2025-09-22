using System.ComponentModel.DataAnnotations;

namespace rastreamentoWorkshopAPI
{
    public class Colaborador
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Nome { get; set; }
        
        public List<AtaColaborador> AtaColaboradores { get; set; }
    }
}

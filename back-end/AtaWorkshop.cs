using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace rastreamentoWorkshopAPI
{
    public class AtaWorkshop
    {
        public int Id { get; set; }

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        
        public List<AtaColaborador> AtaColaboradores { get; set; }
    }
}

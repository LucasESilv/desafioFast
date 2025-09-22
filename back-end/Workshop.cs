using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rastreamentoWorkshopAPI
{
    public class Workshop
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Nome { get; set; }

        public DateTime DataRealizacao { get; set; }

        [StringLength(2000)]
        public string Descricao { get; set; }

    }
}

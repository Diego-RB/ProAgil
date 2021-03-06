using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Dominio
{
    public class Lote
    {
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int quantidade { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; }
       

    }
}
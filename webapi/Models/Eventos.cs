using System.Runtime.Serialization;
using System;
using webapi.Models;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Eventos
    {
        [Key]
        public int EventoId {get;set;}
        public string NomeDoEvento {get;set;}
        public int CapacidadeDoEvento {get;set;}
        public int QuantidadeDeIngressos {get;set;}
        public DateTime DataDoEvento {get;set;}
        public double ValorDoIngresso {get;set;}
        public string GeneroDoEvento{get;set;}
        public int CasaDeShowId {get;set;} 
        public CasaDeShow CasasDeShow {get;set;}
    }
}
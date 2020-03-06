using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Ingresso
    {
        [Key]
        public int IngressoKey {get;set;}
        public int EventosId {get;set;}
        public Eventos Eventos {get;set;}
    }
}
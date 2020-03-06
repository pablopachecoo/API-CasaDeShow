using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class CasaDeShow
    {
        [Key]
        public int CasaDeShowId {get;set;}
        public string Endereco {get;set;}
        
        public string NomeDaCasa {get;set;}
    
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace webapi.Models
{
    public class Usuario 
    {
        [Key]
        public int UsuarioId {get;set;}
        [Required]
        public string UserName {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [IgnoreDataMember]
        public string Senha{get;set;}
    }
}
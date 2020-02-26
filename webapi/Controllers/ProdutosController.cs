using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{    
    [ApiController]
    [Route("listar/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ProdutosController(ApplicationDbContext database){
            this.database = database;
        }


        [HttpGet]
        /*Retorna os Produtos do Banco de Dados numa Lista*/

        public IActionResult PegarProdutos(){
            var produtos = database.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        /*Retorna o Produto com o ID específico*/

        public IActionResult PegarProduto(int id){
            try{
            var produtos = database.Produtos.First(p => p.Id == id);
            return Ok(produtos);
            }
            /*Trata a Possível exceção de um Id Inexistente, junto com uma mensagem descritiva*/

            catch (Exception e) {
                return BadRequest("o id " +  id + " não existe no banco de dados   " + e.Message);        
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProdutoTemp pTemp){
            /* Validação do Preço*/
            if (pTemp.Preco <= 0)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Preço tem que ser maior que zero"});
            }
            /* Validação do Nome*/

            if (pTemp.Nome == "" || pTemp.Nome == null) { Response.StatusCode = 400; return new ObjectResult(new {msg = "O Campo Nome não pode ser deixado em branco"}); }
            
            else {
                Produto p = new Produto();
                p.Nome = pTemp.Nome;
                p.Preco = pTemp.Preco;
                database.Produtos.Add(p);
                database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new {msg = "O Produto |" + p.Nome + "| Foi Criado"});
            }
        }

        [HttpDelete("{id}")]
        /*Deleta o Produto com o Id específico*/
        public IActionResult Deletar(int id){
            try{
            /*Percorre a database dos produtos e retorna o primeiro Produto que o Id coincide o parâmetro imputado*/
            var produtos = database.Produtos.First(p => p.Id == id);
            database.Produtos.Remove(produtos);
            database.SaveChanges();

            /*Mensagem confirmando a deleção*/            
            return Ok("o produto |" + produtos.Nome + "| foi deletado com sucesso");}

            /*Mensagem de erro é mostrada na tela, junto com informações mais detalhadas sobre qual exceção causou o erro*/
            catch (Exception e) {
                return BadRequest("O Produto com o id |" +  id + "| não pode ser deletado" + e.Message);        
            }
        }

        [HttpPatch]
        /*Edição*/
        public IActionResult Patch ([FromBody]Produto produto){
            if (produto.Id > 0){
                try{
                var p = database.Produtos.First(ptemp => ptemp.Id == produto.Id);
                p.Nome = produto.Nome != null ? produto.Nome : p.Nome;
                p.Preco = produto.Preco != 0 ? produto.Preco : p.Preco;                
                database.SaveChanges();
                return Ok("O Produto com o id |" +  p.Id + "| Foi Editado");
                }
                /* Tratando Exceção */
                catch{Response.StatusCode = 400;                
                return new ObjectResult(new {msg = "o produto com o id |" + produto.Id + "| Não exite"});}
            }
            else{
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "Id do produto invalido"});
            }
        }


        public class ProdutoTemp{
            public string Nome {get;set;}
            public float Preco {get;set;}
        }
    }
}
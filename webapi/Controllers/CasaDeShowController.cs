using System.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;


using System.Net.Http;


using webapi.Models;


namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CasaDeShowController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public CasaDeShowController(ApplicationDbContext database){
            this.database = database;
        }

       
        [HttpGet]
        /*Retorna as Casas de Show do Banco de Dados numa Lista*/

        public IActionResult ListarCasas(){            
            var casas = database.CasasDeShow.ToList();
            if (casas.Count.Equals(0))
            {
                return NotFound("Não existe nenhuma casa de Show cadastrada"); /*Erro 404*/
            }            
            return Ok(casas);            
        }

        [HttpGet("{id:int}")]
        /*Retorna a Casa De Show com o ID especificado*/
    
        public IActionResult ListarCasasId(int id){
            if (id.Equals(null))
            {
                return BadRequest("O Campo de Id ficou vazio"); /*Trata a exceção de um Id não imputado, Retornando um Bad Request (ERRO 400)*/
            }
            
            var casaId = database.CasasDeShow.Where(p => p.CasaDeShowId == id).SingleOrDefault();

            if(casaId == null){
                return NotFound("A Casa de show com o Id |" + id + "| não existe"); /*Trata a exceção de um Id Inexistente, Retornando um NotFound (ERRO 404)*/
            }

            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "ERRO 500, não tem como editar por que não existe uma casa de show com esse id");
            }

            else{
                return Ok(casaId);
            }           
            
        }

        [Route("api/asc")]
        [HttpGet]
       
        public IActionResult ListarCasasABC(){  //LISTAR POR ORDEM ALFABÉTICA     
            var casas = database.CasasDeShow.ToList();
            var lista = casas.OrderBy(x=>x.NomeDaCasa).ToList();        
            return Ok(lista);
        }

        [Route("api/desc")]
        [HttpGet]
       
        public IActionResult ListarCasasCBA(){  //LISTAR POR ORDEM ALFABÉTICA SÓ QUE NÃO 
            var casas = database.CasasDeShow.ToList();
            var lista = casas.OrderBy(x=>x.NomeDaCasa).ToList();
            lista.Reverse();
            return Ok(lista);
        } 
        
        [HttpGet("nome/{NomeDaCasa}")]    /*Retorna a Casa com o NOME especificado*/
        public IActionResult ListarCasasNome(string NomeDaCasa){

            if (NomeDaCasa.Equals(null))
            {
                return BadRequest("Insira o Nome da Casa de Show"); /*Trata a exceção de um Nome não imputado, Retornando um Bad Request (ERRO 400)*/
            }
            var casaNome = database.CasasDeShow.Where(p => p.NomeDaCasa == NomeDaCasa).SingleOrDefault();
            if (casaNome == null)
            {
                return NotFound("Uma casa de Show com o nome |" + NomeDaCasa + "| Não existe"); /*Trata a exceção de um Nome não imputado, Retornando um Not Found (ERRO 404)*/
            }            
            return Ok(casaNome);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CasaDeShowTemp casaTemp){

            if (casaTemp.NomeDaCasa.Equals(null) || casaTemp.Endereco.Equals(null))
            {
                return BadRequest("Preencha todos os campos corretamente");                
            }
            else {
                CasaDeShow casa = new CasaDeShow();
                casa.NomeDaCasa = casaTemp.NomeDaCasa;
                casa.Endereco = casaTemp.Endereco;
                database.Add(casa);
                database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult(new {msg = "A Casa De Show |" + casa.NomeDaCasa + "| Foi Criada"});
            }
        }

        [HttpDelete("{id}")]        
        /*Deleta o Produto com o Id específico*/
        public IActionResult Deletar(int id){
            if (id.Equals(null))
            {
                return BadRequest("Preencha todos os Campos Corretamente"); /*Trata a exceção de um Id não imputado, Retornando um Bad Request (ERRO 400)*/
            }           
            /*Percorre a database das Casas e retorna a primeira Casa que o Id coincide o parâmetro imputado*/
            var casa = database.CasasDeShow.Where(p => p.CasaDeShowId == id).SingleOrDefault();
            if (casa is null)
            {
                return NotFound("A Casa de Show o com id |" + id + "| não existe"); /*Trata a exceção de um id inexistente, Retornando um Not Found (ERRO 404)*/
            }
            database.CasasDeShow.Remove(casa);
            database.SaveChanges();
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "ERRO 500, não tem como deletar por que não existe uma casa de show com esse id");
            }
            /*Mensagem confirmando a deleção*/            
            return Ok("A Casa |" + casa.NomeDaCasa + "| foi deletada com sucesso");
        }

        [HttpPut("{id}")]
        /*Edição*/
        public IActionResult Patch ([FromBody]CasaDeShowTemp casaTemp, int id){ ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (casaTemp.NomeDaCasa == null || casaTemp.Endereco == null)
            {
                return BadRequest("Prencha os campos Corretamente"); /*erro 400*/
            }            
            var casa = database.CasasDeShow.Where(ctemp => ctemp.CasaDeShowId == id).SingleOrDefault();
            if (casa is null)
            {
                return NotFound("Nao tem casa de show com esse id");
            }
            casa.NomeDaCasa = casaTemp.NomeDaCasa;
            casa.Endereco = casaTemp.Endereco;
            database.SaveChanges();
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "ERRO 500, não tem como editar por que não existe uma casa de show com esse id");
            }
            return Ok("A Casa De Show com o id |" +  casa.CasaDeShowId + "| Foi Editado");
        }

        public class CasaDeShowTemp{
            public int CasaDeShowId {get;set;}
            public string Endereco {get;set;}
            public string NomeDaCasa {get;set;}
        }
    }
}
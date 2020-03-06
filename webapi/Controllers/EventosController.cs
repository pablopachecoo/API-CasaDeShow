using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {        
        private readonly ApplicationDbContext database;

        public EventosController(ApplicationDbContext database){
            this.database = database;
        }

        
        [HttpGet]
        /*RETORNA OS EVENTOS CADASTRADOS NOME LISTA */
        public IActionResult ListarEventos(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            eventos.AsEnumerable();
            return Ok(eventos);
        }

        [Route("capacidade/asc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELA CAPACIDADE EM ORDEM CRESCENTE*/
        public IActionResult ListarCapacidade(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.CapacidadeDoEvento).ToList();
            return Ok(lista);
        }

        [Route("capacidade/desc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELA CAPACIDADE EM ORDEM DECRESCENTE*/
        public IActionResult ListarCapacidadeReverse(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.CapacidadeDoEvento).ToList();
            lista.Reverse();
            return Ok(lista);
        }

        [Route("data/asc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELA DATA EM ORDEM CRESCENTE*/
        public IActionResult ListarData(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.DataDoEvento).ToList();
            return Ok(lista);
        }

        [Route("data/desc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELA DATA EM ORDEM DECRESCENTE*/
        public IActionResult ListarDataReverse(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.DataDoEvento).ToList();
            lista.Reverse();
            return Ok(lista);
        }

        [Route("nome/asc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELO NOME EM ORDEM CRESCENTE*/
        public IActionResult ListarNomeAsc(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.NomeDoEvento).ToList();
            return Ok(lista);
        }

        [Route("nome/desc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELO NOME EM ORDEM DECRESCENTE*/
        public IActionResult ListarNomeDesc(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.NomeDoEvento).ToList();
            lista.Reverse();
            return Ok(lista);
        }

        [Route("preco/asc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELO VALOR DO INGRESSO EM ORDEM CRESCENTE*/
        public IActionResult ListarValorAsc(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.ValorDoIngresso).ToList();
            lista.Reverse();
            return Ok(lista);
        }

        [Route("preco/desc")]
        [HttpGet]
        /*RETORNA OS EVENTOS PELO PRECO EM ORDEM DECRESCENTE*/
        public IActionResult ListarValorDesc(){
            var eventos = database.Eventos.ToList();
            if (eventos.Count.Equals(0))
            {
                return NotFound("Não tem eventos cadastrados no banco");/*------Tratando 404*/
            }
            var lista = eventos.OrderBy(x=>x.ValorDoIngresso).ToList();
            lista.Reverse();
            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        /*Retorna o eventos com o ID especificado*/
        public IActionResult ListarEventosId(int id){
            if (id.Equals(null))
            {
                return BadRequest("Campo ID Vazio");/*------Tratando ERRO (400)*/
            }
            var eventoId = database.Eventos.Where(e => e.EventoId == id).SingleOrDefault();
            if(eventoId == null){
                return NotFound("O Evento com o id |" + id + "| não existe"); /*Tratando (ERRO 404)*/
            }
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "ERRO 500"); /*Tratando (ERRO 500)*/
            }             
            return Ok(eventoId);            
            /*Trata a Possível exceção de um Id Inexistente, junto com uma mensagem descritiva*/            
        }
        

        [HttpPost]
        public IActionResult Post([FromBody] EventosTemp casaTemp){       
            /* Validação do Nome*/

            if (casaTemp.NomeDoEvento == null || casaTemp.CapacidadeDoEvento.Equals(0) || casaTemp.QuantidadeDeIngressos.Equals(0) || 
            casaTemp.DataDoEvento == null || casaTemp.ValorDoIngresso.Equals(0) || casaTemp.GeneroDoEvento == null)
            {
                return BadRequest("Preencha todos os campos corretamente"); /*Tratando erro 400*/
            }
            Eventos evento = new Eventos();            
            evento.NomeDoEvento = casaTemp.NomeDoEvento;
            evento.CapacidadeDoEvento = casaTemp.CapacidadeDoEvento; 
            evento.QuantidadeDeIngressos = casaTemp.QuantidadeDeIngressos;
            evento.DataDoEvento = casaTemp.DataDoEvento;
            evento.ValorDoIngresso = casaTemp.ValorDoIngresso;
            evento.GeneroDoEvento = casaTemp.GeneroDoEvento;
            evento.CasaDeShowId = casaTemp.CasaDeShowId;
            if (!database.CasasDeShow.Any(e => e.CasaDeShowId == casaTemp.CasaDeShowId))
            {
                return StatusCode(500, "ERRO 500, Não tem como criar um evento, por quê que não existe uma casa com esse id");
            }
            database.Add(evento);
            database.SaveChanges();
            return Ok("O Evento |" + casaTemp.NomeDoEvento + "| Foi Criado");
            
        }

        [HttpDelete("{id}")]
        /*Deleta o Evento com o Id específico*/
        public IActionResult Deletar(int id){
            if (id.Equals(null))
            {
                return BadRequest("Insira o Id corretamente"); /*Tratando erro 400*/
            }
            /*Percorre a database do eventos e retorna o primeiro evento que o Id coincide o parâmetro imputado*/
            var evento = database.Eventos.Where(e => e.EventoId == id).SingleOrDefault();            
            if (evento is null)
            {
                return NotFound("O Evento com o id |" + id + "| não existe");
            }
            database.Eventos.Remove(evento);
            database.SaveChanges();
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "ERRO 500, não tem como deletar por que não existe um evento com esse id");
            }

            /*Mensagem confirmando a deleção*/            
            return Ok("O Evento |" + evento.NomeDoEvento + "| foi deletado com sucesso");

            /*Mensagem de erro é mostrada na tela, junto com informações mais detalhadas sobre qual exceção causou o erro*/
            
                //return BadRequest("O Evento com o id |" +  id + "| não pode ser deletado" + e.Message);        
            
        }

        [HttpPut("{id}")]
        /*Edição*/
        public IActionResult EditarEvento ([FromBody]EventosTemp evento, int id){
            if (evento.NomeDoEvento == null || evento.CapacidadeDoEvento.Equals(0) || evento.QuantidadeDeIngressos.Equals(0)
                   || evento.DataDoEvento == null || evento.ValorDoIngresso.Equals(0) || evento.GeneroDoEvento == null)
            {
                return BadRequest("Preencha todos os campos corretamente"); /*Tratando o erro 400*/
            }
                
            var e = database.Eventos.Where(etemp => etemp.EventoId == id).SingleOrDefault();
            if (e is null)
            {
                return NotFound("O Evento com o id |" + id + "| não existe"); /*Tratando o erro 404*/
            }            
            e.NomeDoEvento = evento.NomeDoEvento;
            e.CapacidadeDoEvento = evento.CapacidadeDoEvento; 
            e.QuantidadeDeIngressos = evento.QuantidadeDeIngressos;
            e.DataDoEvento = evento.DataDoEvento;
            e.ValorDoIngresso = evento.ValorDoIngresso;
            e.GeneroDoEvento = evento.GeneroDoEvento;
            e.CasaDeShowId = evento.CasaDeShowId;            
            
            database.SaveChanges();
                       
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "ERRO 500,Não tem como editar por que não existe um evento com esse id");
            }
            return Ok("O Evento com o Id |" +  e.EventoId + "| Foi Editado");  /*Tratando erro 500*/
            
        }

        public class EventosTemp{
            public int EventoId {get;set;}
            public string NomeDoEvento {get;set;}
            public int CapacidadeDoEvento {get;set;}
            public int QuantidadeDeIngressos {get;set;}
            public DateTime DataDoEvento {get;set;}
            public double ValorDoIngresso {get;set;}
            public string GeneroDoEvento{get;set;}
            public int CasaDeShowId {get;set;} 
            }
        
    }
}
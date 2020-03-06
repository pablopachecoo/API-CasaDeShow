using System.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/vendas")]
    public class IngressoController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public IngressoController(ApplicationDbContext database)
        {
            this.database = database;
        }

       
        [HttpGet]
        /*Retorna os usuarios*/
        public IActionResult ListarIngressos(){            
            var ingressos = database.Ingressos.ToList();
            if (ingressos == null)
            {
                return NotFound("Não existe nenhum usuário cadastrado"); /*Erro 404*/
            }
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "Erro 500");
            }
            return Ok(ingressos);            
            }
        [HttpGet("{id}")]
        /*Retorna os usuarios*/
        public IActionResult ListarIngressosId(int id)
        {
            if (id.Equals(null))
            {
                return BadRequest("O Campo de Id ficou vazio"); /*Tratando a exceção, quando um id não é imputado, retorna um Bad Request (ERRO 400)*/
            }         
            var ingressos = database.Ingressos.Where(p => p.IngressoKey == id).SingleOrDefault();            
            if (ingressos == null)
            {
                return NotFound("Não existe nenhum usuário cadastrado com esse Id"); /*Tratando a exceção, quando um id imputado inexistir, retorna o erro NOT FOUND (404)*/
            }
            return Ok(ingressos);            
        }
    }
}

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
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public UsuarioController(ApplicationDbContext database)
        {
            this.database = database;
        }

       
        [HttpGet]
        /*Retorna os usuarios*/
        public IActionResult ListarUsuarios(){            
            var users = database.Usuarios.ToList();
            if (users == null)
            {
                return NotFound("Não existe nenhum usuário cadastrado"); /*Erro 404*/
            }
            if (Response.StatusCode.Equals(500))
            {
                return StatusCode(500, "Erro 500");
            }
            return Ok(users);            
            }
        [HttpGet("{id}")]
        /*Retorna os usuarios*/
        public IActionResult ListarUsuariosId(int id)
        {
            if (id.Equals(null))
            {
                return BadRequest("O Campo de Id ficou vazio"); /*Tratando a exceção, quando um id não é imputado, retorna um Bad Request (ERRO 400)*/
            }         
            var users = database.Usuarios.Where(p => p.UsuarioId == id).SingleOrDefault();            
            if (users == null)
            {
                return NotFound("Não existe nenhum usuário cadastrado com esse Id"); /*Tratando a exceção, quando um id imputado inexistir, retorna o erro NOT FOUND (404)*/
            }
            
            var e = users.Senha;
            return Ok(users);            
        }
    }
}

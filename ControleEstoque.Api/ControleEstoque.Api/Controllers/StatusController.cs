using ControleEstoque.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleEstoque.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly DapperContext _context;

        public StatusController(DapperContext context)
        {
            _context = context;
        }

        [HttpGet("db-status")]
        public IActionResult GetDbStatus()
        {
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                    return Ok("✅ Conexão com o banco de dados estabelecida com sucesso!");

                return StatusCode(500, "❌ Conexão não pôde ser aberta.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Erro ao conectar ao banco: {ex.Message}");
            }
        }
    }
}

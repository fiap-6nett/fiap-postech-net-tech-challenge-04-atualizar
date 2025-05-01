using Contato.Atualizar.Application.Dtos;
using Contato.Atualizar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contato.Atualizar.Web.Controllers;

[Route("api/[controller]")]
public class ContatoController : ControllerBase
{
    private readonly ILogger<ContatoController> _logger;
    private readonly IContatoService _contatoService;

    public ContatoController(ILogger<ContatoController> logger, IContatoService contatoService)
    {
        _logger = logger;
        _contatoService = contatoService;
    }
    
    /// <summary>
    /// Enviar um contato para a fila de atualização.
    /// </summary>
    /// <param name="dto">Dados do contato a serem atualizados.</param>
    /// <returns>Retorna o ID e a data da requisição.</returns>
    /// <response code="200">Contato atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPut("[action]")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarContato([FromBody] AtualizarContatoDto dto)
    {
        try
        {
            _logger.LogInformation($"Acessou {nameof(AtualizarContato)}. Entrada: {dto}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Dados inválidos - Entrada: {dto}");
                return BadRequest(ModelState);
            }

            await _contatoService.AtualizarContatoAsync(dto);
            _logger.LogInformation($"Dados enviados para fila com sucesso.");

            var response = new ResponseDto()
            {
                Id = dto.Id,
                DataCriacao = DateTime.Now
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha na api AtualizarContato. Erro{ex}");
            return StatusCode(500, $"Internal server error - {ex}");
        }
        
    }
}
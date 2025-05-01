using Contato.Atualizar.Application.Dtos;

namespace Contato.Atualizar.Application.Interfaces;

public interface IContatoService
{
    Task AtualizarContatoAsync(AtualizarContatoDto dto);
}
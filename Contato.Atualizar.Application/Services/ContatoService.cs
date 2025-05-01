using Contato.Atualizar.Application.Dtos;
using Contato.Atualizar.Application.Interfaces;
using Contato.Atualizar.Domain.Entities;

namespace Contato.Atualizar.Application.Services;

public class ContatoService : IContatoService
{
    private readonly IRabbitMqProducer _producer;
    
    public ContatoService(IRabbitMqProducer producer)
    {
        _producer = producer;
    }
    
    public Task AtualizarContatoAsync(AtualizarContatoDto dto)
    {
         var contato = new ContatoEntity();
         
         contato.SetId(dto.Id);
         contato.SetNome(dto.Nome);
         contato.SetTelefone(dto.Telefone);
         contato.SetEmail(dto.Email);
         contato.SetDdd(dto.Ddd);
         
         _producer.EnviarMensagem(contato);
         
         return Task.CompletedTask;
    }
}
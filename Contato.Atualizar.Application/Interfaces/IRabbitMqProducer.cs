namespace Contato.Atualizar.Application.Interfaces;

public interface IRabbitMqProducer
{
    void EnviarMensagem(object mensagem);
}
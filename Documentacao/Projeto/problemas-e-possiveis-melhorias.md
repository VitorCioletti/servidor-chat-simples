# Projeto

## Problemas e possíveis melhorias

* [Acoplamento de Regras com Redis](#acoplamento-de-Regras-com-WebSocket)
* [Acoplamento de Regras com WebSocket](#acoplamento-de-Regras-com-Redis)
* [Falta de verificação de formato dos campos do protocolo](#falta-de-verificação-de-formato-dos-campos-do-protocolo)
* [Falta de reuso nas classes de regras Contexto](#falta-de-reuso-nas-classes-de-regras-Contexto)
* [Falta de finalização graciosa](#falta-de-finalização-graciosa)
* [Mensagens fire and forget](#mensagens-fire-and-forget)
* [Obrigatoriedade de configuração de ambiente de desenvolvimento](#obrigatoriedade-de-configuração-de-ambiente-de-desenvolvimento)
* [Protocolo obriga que as ações sirvam para todos os contextos](#protocolo-obriga-que-as-ações-sirvam-para-todos-os-contextos)


### Acoplamento de Regras com Redis
A regra de negócio está diretamente acoplada com o estabelecimento de conexão com `Redis`, dificultando reuso do código.
Uma possível solução é abstrair a escrita de dados com o Redis através, por exemplo, de injeção de dependência.

### Acoplamento de Regras com WebSocket

A regra de negócio está diretamente acoplada com `Websocket` dificultando reuso do código. Uma possível solução é abstrair a escrita de dados com o Redis através, por exemplo, de injeção de dependência.

### Falta de verificação de formato dos campos do protocolo

Não é feita nenhuma verificação de formato dos campos do protocolo. Uma forma de resolver esse problema é o parser aplicar um `json schema` ao `Deserializar` e `Serializar` os objetos.

### Falta de finalização graciosa

Não foi desenvolvido nenhum mecanismo de finalização graciosa, isso faz com que mensagens possam ser perdidas no meio do processamento.

### Falta de reuso nas classes de regras Contexto

É observado que os códigos dentro dos `switch case` de `Usuario` de `Sala` são bem parecidos. Deve haver uma forma de simplifcar os códigos duplicados através da leitura do objeto do protocolo `Requisicao` e abstraindo a execução dos contextos. É necessário pensar mais.

### Mensagens fire and forget

Da forma como está estruturado, o protocolo só suporta mensagens `fire and forget`, apesar de haver resposta de sucesso do servidor, é impossível saber qual resposta de sucesso é de qual mensagens ao as enviar paralelamente. Uma possível solução é o cliente dar um id único na mensagem ao envia-la e, ao confirmar envio, o servidor deve devolver a confirmação com este id único.

### Obrigatoriedade de configuração de ambiente de desenvolvimento

Para rodar o projeto, é necessário instalar o Redis e .NET na máquina. Uma possível solução para isso é `dockerizar` (não foi implementado por falta de experiência).

### Protocolo obriga que as ações sirvam para todos os contextos

Uma limitação do protocolo é que ele obriga que todas as ações criadas sirvam para todos os contextos possíveis. Seria possível implementar uma ação que está disponível apenas para um contexto, porém, deverá ser necessário processamento dessas regras pelo Parser.
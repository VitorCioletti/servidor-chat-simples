# Projeto

## Estrutura

* [Configuração](#configuração)
* [Protocolo](#protocolo)
* [Redis](#redis)
* [Regras](#regras)

## Configuração

Responsável por configurar `Log`, `ArquivoConfiguracao` e `WebSocket`.

### Protocolo

Responsável por serializar e deserializar a mensagem recebida pelo `WebSocket` e a respectiva resposta do servidor.

## Regras

Responsável por obter as mensagens recebidas pelo `WebSocket` e parseadas pelo `Protocolo` e processa as regras de negócio.

## Redis

Responsável por interagir com o `Redis`, abstraindo as chaves criadas a partir de propriedades.
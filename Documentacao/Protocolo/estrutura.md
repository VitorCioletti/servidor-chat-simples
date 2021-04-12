# Protocolo

## Estrutura

* [Conceitos básicos](#conceitos-básicos)
    * [Contextos criados](#contextos-criados)
    * [Ações criadas](#ações-criadas)
* [Exemplos](#exemplos)
    * [Requisição de criação de usuário](#requisição-de-criação-de-usuário)
    * [Requisição de criação de sala](#requisição-de-criação-de-sala)
    * [Requisição de envio de mensagem para sala](#requisição-de-envio-de-mensagem-para-sala)
    * [Resposta de erro](#resposta-de-erro)
    * [Resposta de sucesso](#resposta-de-sucesso)
    * [Recebimento de mensagem](#recebimento-de-mensagem)


### Conceitos básicos

O protocolo funciona no formato `JSON` a partir de passagem de parâmetros no "Corpo" para execução de uma "Ação" num determinado "Contexto".

**O que é um "Contexto"?** É uma separação lógica da estrutura do chat.

**O que é uma "Ação"?** É a execução de uma ação num determinado contexto. (Diferentes contextos tem conceitos diferentes para uma mesma ação)

**O que é um "Corpo"?** São os parâmetros passados para a execução da ação.

#### Contextos criados

Id      | Descrição
--      | --------- 
Usuario | Representa os usuários.
Sala    | Represtenta as salas.

#### Ações criadas

Id            | Descrição
--            | --------- 
Adiciona      | Adiciona um regitro no contexto. 
Remove       | Remove um registro no contexto. 
EnviaMensagem | Envia uma mensagem no contexto. 
Entra         | Entra no contexto. 
Sai           | Sai do contexto. 

### Exemplos

#### Requisição de criação de usuário
```
{
    "Contexto": "Usuario",
    "Acao": "Adiciona",
    "Corpo": {
        "ApelidoUsuario": "novo-usuario"
    }
}
```

#### Requisição de criação de sala
```
{
    "Contexto": "Sala",
    "Acao": "Adiciona",
    "Corpo": {
        "ApelidoUsuario": "novo-usuario",
        "IdSala": "nova-sala"
    }
}
```

#### Requisição de envio de mensagem para sala 
```
{
    "Contexto": "Sala",
    "Acao": "EnviaMensagem",
    "Corpo": {
        "ApelidoUsuario": "novo-usuario",
        "IdSala": "nova-sala",
        "Texto": "Texto para todos os membros da sala."
    }
}
```

#### Resposta de erro
```
{
    "Sucesso": false,
    "Corpo": { 
        "IdErro": "usuario-ja-existe",
        "MensagemErro": "Usuario já existe na base."
    }
}
```

#### Resposta de sucesso
``` 
{
    "Sucesso": true,
    "Corpo": { }
}
```

#### Recebimento de mensagem
```
{
    "Id":"e0ebf2b9-5687-443d-80ee-94f446737393",
    "IdSala":"sala",
    "ApelidoUsuario":"outro-usuario",
    "DataHoraEnvio": 1618190438, 
    "Texto":"Mensagem de um usuário."
}
```
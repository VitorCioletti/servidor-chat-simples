# Projeto

## Como rodar e testar

### Como rodar
O tutorial foi realizado num "Ubuntu 20.04";
```
// instalar Redis, node e NPM
sudo apt install redis-tools redis-server node npm

// instalar WSCAT para estabelecer conexões WebSocket
npm install -g wscat

// buildar o projeto
dotnet build

// rodar o projeto
dotnet run
```

### Como testar

Não foi desenvolvimento nenhum cliente para conversar com o servidor, é necessário conectar manualmente. Este tutorial utiliza [`wscat`](https://www.npmjs.com/package/wscat).

OBS: Abrir dois terminais para realizar o teste:

#### Criação do `usuario-um-teste`
- Estabelecer conexão: ``` wscat -c <ENDERECO>:<PORTA> ```
- Criar usuário: ```{ "Contexto": "Usuario", "Acao": "Adiciona", "Corpo": { "ApelidoUsuario": "usuario-um-teste" } }```
- Logar com usuário: ```{ "Contexto": "Usuario", "Acao": "Entra", "Corpo": { "ApelidoUsuario": "usuario-um-teste" } }```
- Criar sala: ```{ "Contexto": "Sala", "Acao": "Adiciona", "Corpo": { "ApelidoUsuario": "usuario-um-teste", "IdSala": "nova-sala"} }```
- Entrar na sala: ```{ "Contexto": "Sala", "Acao": "Entra", "Corpo": { "ApelidoUsuario": "usuario-um-teste", "IdSala": "nova-sala"} }```

#### Criação do `usuario-dois-teste`
- Estabelecer conexão: ``` wscat -c <ENDERECO>:<PORTA> ```
- Criar usuário: ```{ "Contexto": "Usuario", "Acao": "Adiciona", "Corpo": { "ApelidoUsuario": "usuario-dois-teste" } }```
- Logar com usuário: ```{ "Contexto": "Usuario", "Acao": "Entra", "Corpo": { "ApelidoUsuario": "usuario-dois-teste" } }```
- Entrar na sala: ```{ "Contexto": "Sala", "Acao": "Entra", "Corpo": { "ApelidoUsuario": "usuario-dois-teste", "IdSala": "nova-sala"} }```
- Enviar mensagem para sala: ```{ "Contexto": "Sala", "Acao": "EnviaMensagem", "Corpo": { "ApelidoUsuario": "usuario-dois-teste", "IdSala": "nova-sala", "Texto": "Mensagem para sala."} }```






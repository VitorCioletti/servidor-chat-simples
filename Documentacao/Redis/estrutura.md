# Redis

## Estrutura

Chave                     | Tipo de dado | Descrição
-----                     | ------------ | --------- 
salas-ativas              | set          | IDs de salas ativas.
usuarios-cadastrados      | set          | Apelidos de usuários cadastrados no sistema. 
usuarios-online           | hash         | Apelido de usuário online e seu id de conexão.
usuarios-sala:{nome-sala} | set          | Apelidos de usuários numa sala.       
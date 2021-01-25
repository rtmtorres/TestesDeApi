# API PARA CADASTRO DE CLIENTES

Para atender aos requisitos propostos, foi pensado em criar uma arquitetura hexagonal, fácil de criar novas implementações, tentei também seguir o modelo CQRS para modificações de estado da aplicação, assim como para leitura utilizando o dapper.

## Como testar a aplicação?

Para testar a aplicação e verificar sua cobertura de testes, assim com problemas de código, será necessário levantar o Sonarqube:

```
docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube
```

Após isso, configure um projeto e um token dentro do sonar, com isso, altere o arquivo <mark>docker-compose.tests.yml</mark>, alterando os argumentos:
- SONARQUBE_TOKEN
- SONARQUBE_PROJECT

Em seguida, é só executar o shell <mark>tests.sh</mark>.

Com isso, poderá visualizar os dados do projeto no Sonarqube, ou então acessar a pasta <mark>testresults</mark> dentro da raiz do projeto.


## Como rodar a aplicação?

Precisa ter o docker instalado e rodar o shell <mark>run.sh</mark>

O tempo para que a API responda corretamente poderá demorar alguns minutos, por causa da dependencia do MYSQL e do migration do flyway.

Assim que o MYSql estiver funcionando, o Flyway irá fazer o migration do banco, criando suas tabelas.


#### Endpoints disponíveis:
* http://localhost:7001 (Api de Cadastro de Clientes)
* http://localhost:7002 (Gerenciamento do Mysql - adminer)

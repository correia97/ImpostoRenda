[![Build Status](https://dev.azure.com/pauloc/ImpostoRenda/_apis/build/status/Build%20Test?branchName=master)](https://dev.azure.com/pauloc/ImpostoRenda/_build/latest?definitionId=1&branchName=master) [![CircleCI](https://circleci.com/gh/correia97/ImpostoRenda.svg?style=shield)](https://circleci.com/gh/correia97/ImpostoRendaLB3) [![Git Buil](https://github.com/correia97/ImpostoRendaLB3/workflows/Git%20Buil/badge.svg) ](https://github.com/correia97/ImpostoRendaLB3/workflows) [![Build Status](https://travis-ci.org/correia97/ImpostoRendaLB3.svg?branch=master)](https://travis-ci.org/correia97/ImpostoRendaLB3) [![Build status](https://ci.appveyor.com/api/projects/status/smik91jmcfefb1kx/branch/master?svg=true)](https://ci.appveyor.com/project/correia97/impostorenda/branch/master)

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/484dde6a9ba340cd8fcecb19557461e9)](https://www.codacy.com/manual/correia97/ImpostoRenda?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=correia97/ImpostoRenda&amp;utm_campaign=Badge_Grade) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=correia97_ImpostoRenda&metric=coverage)](https://sonarcloud.io/dashboard?id=correia97_ImpostoRenda) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=correia97_ImpostoRenda&metric=alert_status)](https://sonarcloud.io/dashboard?id=correia97_ImpostoRenda) [![codecov](https://codecov.io/gh/correia97/ImpostoRenda/branch/master/graph/badge.svg)](https://codecov.io/gh/correia97/ImpostoRenda) 
# ImpostoRenda

API em .Net Core para um simples consulta do valor de desconto do IR mensal sobre um salário.

Principais itens | versão
------------- | -------------
 .Net Core | 3.1
  XUnit | 2.4.2
  FluentAssertions | 5.10.3
  Moq| 4.14.5
  MongoDB | 4.1.13
  Docker | 2.1.0.1

### Requisitos para executar o projeto

  - .Net Core 2.2
  - MongoDb
  - Docker

### Executando o projeto

#### Via Linha de Comando

Estou utilizando o MongoDb em um container Docker utilizando os comandos abaixo
Criação do volume que será utilizado para armazenar os dados do Docker

```bash
docker volume create vMongo
```

Execução do container do MongoDb utilizando a porta padrão 27017 e o volume vMongo que foi previamente criado

```bash
docker run -d -p 27017:27017 --name mongodev -v vMongo:/data/db mongo:4.1.13
```

No caso onde o container não vai ser utilizado no docker ou vai ser utilizado em outra porta é necessário colocar os dados corretos no arquivo

```bash
src\\API\\ImpostoRenda.API\\appsettings.Development.json
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "MongoSettings": {
    "connection": "mongodb://localhost:27017/imposto",
    "baseName": "imposto"
  }
}
```

No diretorio raiz onde foi clonado o projeto abrir o CMD ou PowerShell e executar os comandos abaixo

```bash
 dotnet restore
 dotnet build
 dotnet test
```

Para executar a API

```bash
 dotnet run -p src/API/ImpostoRenda.API/ImpostoRenda.API.csproj
```

Verfificar qual a porta que a API está sendo executada e configurar o endereço correto no arquivo

```bash
src\\WEB\\ImpostoRenda.Web\\appsettings.Development.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AllowedHosts": "*",
  "api": "http://localhost:5716"
}
```

Abrir uma nova janela do CMD ou powershell e executar o projeto MVC

```bash
dotnet run -p src/WEB/ImpostoRenda.WEB/ImpostoRenda.WEB.csproj
```

#### Via Docker

Na pasta raiz do Projeto executar os comandos abaixo para fazer o build do projeto, executar os testes e gerar os binarios em modo release

```bash
dotnet restore
dotnet build
dotnet test
dotnet publish -o out -c Release
```

Criar as imagens Docker com o projeto e executar os container

```bash
docker-compose build
docker-compose up
```
A API estara sendo executada na porta 3001 e o projeto MVC na porta 3002

Rodando os testes via newman

```bash
npm install -g newman
newman run ImpostoRenda.postman_collection.json -n 10 -r cli,json --reporter-json-export ./result/result.json
```
obs.: Será gerado uma pasta result com o arquivo result.json do teste

#### Via Visual Studio

Conforme mencionado na parte da execução via linha de comando o projeto depende de uma banco MongoDb que está sendo utilizado no Container Docker seguir os passos descritos para criação do container ou alterar a configuração para um banco existente.

- Abrir a solução ImpostoRenda.sln
- Clicar com o botão direito sobre a solução -> Properties
- Common Properties -> Startup Project
- Selecionar a opção Multiple startup projects
- Na coluna **Action** dos Projetos API e WEB selecionar a opção **Start**
- Configuration Properties -> Configuration
- Na coluna Configuration deixar em modo Debug
- Executar o projeto apertando a telha F5 ou no menu o botão start

#### Pipeline no Azure DevOps

```bash
azure-pipelines.yml
```

#### Pipeline no CIRCLECI

```bash
.circleci/config.yml
```

#### Pipeline no GIT Actions

```bash
.github/workflows/build.yml
```

### Referências para configuração do Docker e das pipelines

[Renato Groffe](https://github.com/renatogroffe)

[Luiz Carlos](https://github.com/luizcarlosfaria)

[Milton Camara](https://github.com/miltoncamara)

[Circleci Docs](https://circleci.com/docs/)

[CodeCov Docs](https://docs.codecov.io/docs)

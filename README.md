# ImpostoRenda

API em .Net Core para um simples consulta do valor de desconto do IR mensal sobre um salário.

Principais itens | versão
------------- | -------------
 .Net Core | 2.2
  XUnit | 2.4.1
  FluentAssertions | 5.8.0
  Moq| 4.12.0
  MongoDB | 4.1.13
  Docker | 2.1.0.1

### Pré Requisitos para executar o projeto:
  - .Net Core 2.2
  - MongoDb
  - Docker 

### Executando o projeto

#### Via Linha de Comando
Estou utilizando o MongoDb em um container Docker utilizando os comandos abaixo
Criação do volume que será utilizado para armazenar os dados do Docker
<pre>
docker volume create vMongo
</pre>
Execução do container do MongoDb utilizando a porta padrão 27017 e o volume vMongo que foi previamente criado
<pre>
docker run -d -p 27017:27017 --name mongodev -v vMongo:/data/db mongo:4.1.13
</pre>
No caso onde o container não vai ser utilizado no docker ou vai ser utilizado em outra porta é necessário colocar os dados corretos no arquivo
<pre>
src\API\ImpostoRendaLB3.API\appsettings.Development.json
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
</pre>

No diretorio raiz onde foi clonado o projeto abrir o CMD ou PowerShell e executar os comandos abaixo
<pre>
 dotnet restore
 dotnet build
 dotnet test
</pre>
Para executar a API 
<pre>
 dotnet run -p src/API/ImpostoRendaLB3.API/ImpostoRendaLB3.API.csproj
</pre>

Verfificar qual a porta que a API está sendo executada e configurar o endereço correto no arquivo
<pre>
src\WEB\ImpostoRendaLB3.Web\appsettings.Development.json

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
</pre>
Abrir uma nova janela do CMD ou powershell e executar o projeto MVC
<pre>
dotnet run -p src/WEB/ImpostoRendaLB3.WEB/ImpostoRendaLB3.WEB.csproj
</pre>

#### Via Docker
Na pasta raiz do Projeto executar os comandos abaixo para fazer o build do projeto, executar os testes e gerar os binarios em modo release
<pre>
dotnet restore
dotnet build
dotnet test
dotnet publish -o out -c Release
</pre>
Criar as imagens Docker com o projeto e executar os container 
<pre>
docker-compose build
docker-compose up
</pre>
A API estara sendo executada na porta 3001 e o projeto MVC na porta 3002

#### Via Visual Studio
Conforme mencionado na parte da execução via linha de comando o projeto depende de uma banco MongoDb que está sendo utilizado no Container Docker seguir os passos descritos para criação do container ou alterar a configuração para um banco existente.

- Abrir a solução ImpostoRendaLB3.sln
- Clicar com o botão direito sobre a solução -> Properties
- Common Properties -> Startup Project
- Selecionar a opção Multiple startup projects
- Na coluna **Action** dos Projetos API e WEB selecionar a opção **Start**
- Configuration Properties -> Configuration
- Na coluna Configuration deixar em modo Debug
- Executar o projeto apertando a telha F5 ou no menu o botão start



#### Pipeline no Azure DevOps

<pre>azure-pipelines.yml</pre>

#### Pipeline no CIRCLECI
<pre>
.circleci/config.yml
</pre>


### Referências para configuração do Docker e das pipelines

[Renato Groffe](https://github.com/renatogroffe)

[Luiz Carlos](https://github.com/luizcarlosfaria) 

[Milton Camara](https://github.com/miltoncamara)

[Circleci Docs](https://circleci.com/docs/)

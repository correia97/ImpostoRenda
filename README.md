# ImpostoRendaLB3

API em .Net Core para um simples consulta do valor de desconto do IR mensal sobre um salário.

  - Utilizado .Net Core 2.0
  - XUnit
  - FluentAssertions
  - Moq
  - MongoDB

Pré Requisitos para executar o projeto:
  - Uma base de dados MongoDB (o site [mLab](https://mlab.com/) oferece uma base online free)
  - O Microsoft framework core 2.0

Executando o projeto
 - Configure os dados da sua base de dados no arquivo appsettings.json que está no projeto API
 - Restaure os pacotes nuget (dotnet restore na raiz do repositório)
 - Execute o build do projeto ImpostoRendaLB3.API (na raiz da pasta do projeto executar o comando dotnet build)
 - Execute do projeto ImpostoRendaLB3.API (na raiz da pasta do projeto executar o comando dotnet run)
 - Acesse a URL http://localhost:5717/swagger (Obs.: 5717 foi a porta padrão na minha máquina o comando utilize a porta informada no resultado da comando dotnet run)
 - Para interromper a execução na janela de comando execute o comando ctrl+c

Executando os testes
 - Restaure os pacotes nuget (dotnet restore na raiz do repositório)
 - Execute o build do projeto ImpostoRendaLB3.UnitTests (na raiz da pasta do projeto executar o comando dotnet build)
 - Execute do projeto ImpostoRendaLB3.UnitTests (na raiz da pasta do projeto executar o comando dotnet test)


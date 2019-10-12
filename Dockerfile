FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env

# Install Java.
RUN apt-get update
RUN apt-get install -y openjdk-8-jre
RUN rm -rf /var/lib/apt/lists/*

# Define commonly used JAVA_HOME variable
ENV JAVA_HOME /usr/lib/jvm/java-7-openjdk-amd64

# Instala o sonnar
RUN dotnet tool install --global dotnet-sonarscanner --version 4.7.1
# Instala o coverlet
RUN dotnet tool install --global coverlet.console
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /app
# Copiar os arquivos da solution para o container
COPY  . ./

ARG sonarLogin
RUN echo $sonarLogin
# Start do scanner
RUN dotnet sonarscanner begin /k:"correia97_ImpostoRendaLB3" /o:"correia97" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login=$sonarLogin
# Executa o restore
RUN dotnet restore
# Executa os teste
RUN dotnet test
# Realiza a Analise
RUN dotnet sonarscanner end /d:sonar.login=$sonarLogin   
# Publica a Aplicação
RUN dotnet publish -c Release -o out
# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9 as API
WORKDIR /app
COPY --from=build-env /app/src/API/ImpostoRendaLB3.API/out .
ENTRYPOINT [ "dotnet","ImpostoRendaLB3.API.dll","--environment=docker"  ]
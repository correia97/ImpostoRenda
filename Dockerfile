FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env
# expor ferramenta

RUN export PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-sonarscanner
RUN dotnet tool install --global coverlet.console
#RUN chmod +x /root/.dotnet/tools/.store/dotnet-sonarscanner/4.3.1/dotnet-sonarscanner/4.3.1/tools/netcoreapp2.1/any/sonar-scanner-3.2.0.1227/bin/sonar-scanner
#RUN cat /root/.dotnet/tools/.store/dotnet-sonarscanner/4.3.1/dotnet-sonarscanner/4.3.1/tools/netcoreapp2.1/any/sonar-scanner-3.2.0.1227/bin/sonar-scanner

RUN apt update 
RUN echo "-------------------------------------------------------------"
RUN apt list --upgradable 
RUN echo "-------------------------------------------------------------"
RUN apt -y install default-jre 
RUN echo "-------------------------------------------------------------"

RUN update-alternatives --config java
RUN echo "-------------------------------------------------------------"
ENV JAVA_HOME /usr/lib/jvm/java-8-openjdk-amd64/jre/bin/java
RUN echo "-------------------------------------------------------------"
RUN echo $JAVA_HOME



WORKDIR /app
# Copiar os arquivos da solution para o container
COPY  . ./
# Instala o sonnar
#RUN dotnet tool install --global dotnet-sonarscanner --version 4.3.1

# Start do scanner
RUN dotnet sonarscanner begin /k:"correia97_ImpostoRendaLB3" /d:sonar.organization="correia97" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login=$sonarLogin
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
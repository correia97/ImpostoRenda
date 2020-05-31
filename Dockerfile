FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env

#RUN cat /etc/*release

# Atualiza as ferramentas linux e instala o JAVA
RUN apt-get update && \
    apt-get install -y \
    ca-certificates  \
    apt-transport-https \
    openjdk-8-jdk && \
    rm -rf /var/lib/apt/lists/*

RUN curl -O https://download.java.net/java/GA/jdk13/5b8a42f3905b406298b72d750b6919f6/33/GPL/openjdk-13_linux-x64_bin.tar.gz
RUN tar xvf openjdk-13_linux-x64_bin.tar.gz
RUN mv jdk-13 /usr/lib/jvm/

# Configura o JAVA
RUN update-alternatives --config java
RUN export JAVA_HOME=/usr/lib/jvm/jdk-13
ENV JAVA_HOME /usr/lib/jvm/jdk-13
ENV PATH=${JAVA_HOME}/bin:$PATH 

# Instala o sonnar
RUN dotnet tool install --global dotnet-sonarscanner --version 4.9.0
# Variavel de path das ferramentas
ENV PATH="${PATH}:/root/.dotnet/tools"
# Define a pasta onde vai estar os arquivos
WORKDIR /app
# Copiar os arquivos da solution para o container
COPY  . ./
# Argumento informado durante o build
ARG sonarLogin
# Argumento informado durante o build
ARG codecovToken
# Executa o restore
RUN dotnet restore
# Start do scanner
RUN dotnet sonarscanner begin /k:"correia97_ImpostoRendaLB3" \
                              /o:"correia97" /d:sonar.host.url="https://sonarcloud.io" \
                              /d:sonar.login=$sonarLogin \
                              /d:sonar.cs.opencover.reportsPaths="/app/Tests/ImpostoRendaLB3.UnitTests/coverage.opencover.xml" \
                              /d:sonar.exclusions=**/*.js,**/*.css,**/obj/**,**/*.dll,**/*.html,**/*.cshtml,*-project.properties

#Faz o build
RUN dotnet build
# Executa os teste
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --no-build
# Realiza a Analise
RUN dotnet sonarscanner end /d:sonar.login=$sonarLogin   
# Cobertura no CodCov

RUN curl -o codecov.sh https://codecov.io/bash
RUN curl -so codecovenv https://codecov.io/env
RUN chmod +x codecov.sh
RUN chmod +x codecovenv
ENV ci_env=./codecovenv
# Cobertura no CodCov
RUN ./codecov.sh -f "/app/Tests/ImpostoRendaLB3.UnitTests/coverage.opencover.xml" -t $codecovToken
# Publica a Aplicação
RUN dotnet publish -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9 as API
# Define a pasta onde vai estar os arquivos
WORKDIR /app
# Copia os arquivos publicados do container de build para o container final
COPY --from=build-env /app/src/API/ImpostoRendaLB3.API/out .
# Variavél de ambiente que define onde a aplicação está rodando
ENV ASPNETCORE_ENVIRONMENT=docker
# Define qual o executavel do container
ENTRYPOINT [ "dotnet","ImpostoRendaLB3.API.dll"  ]

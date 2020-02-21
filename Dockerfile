FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env

# Atualiza as ferramentas linux e instala o JAVA
RUN apt-get update && \
    apt-get install -y \
    openjdk-8-jdk 

# Configura o JAVA
RUN update-alternatives --config java
RUN export JAVA_HOME=/usr/lib/jvm/java-8-openjdk-amd64
ENV JAVA_HOME /usr/lib/jvm/java-8-openjdk-amd64
ENV PATH=${JAVA_HOME}/bin:$PATH 

# Instala o sonnar
RUN dotnet tool install --global dotnet-sonarscanner --version 4.7.1
ENV PATH="${PATH}:/root/.dotnet/tools"
# Define a pasta onde vai estar os arquivos
WORKDIR /app
# Copiar os arquivos da solution para o container
COPY  . ./
# Argumento informado durante o build
ARG sonarLogin
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
# Publica a Aplicação
RUN dotnet publish -c Release -o out
# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9 as API
# Define a pasta onde vai estar os arquivos
WORKDIR /app
# Copia os arquivos publicados do container de build para o container final
COPY --from=build-env /app/src/API/ImpostoRendaLB3.API/out .
# Define qual o executavel do container
ENTRYPOINT [ "dotnet","ImpostoRendaLB3.API.dll","--environment=docker"  ]

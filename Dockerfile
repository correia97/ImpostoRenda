FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env

RUN apt-get update
RUN apt-get install -y --no-install-recommends apt-utils
# Install Java.
RUN apt-get install -y openjdk-8-jdk
RUN update-alternatives --config java
RUN export JAVA_HOME=/usr/lib/jvm/java-8-openjdk-amd64

# Define commonly used JAVA_HOME variable
ENV JAVA_HOME /usr/lib/jvm/java-8-openjdk-amd64
ENV PATH=${JAVA_HOME}/bin:$PATH 

# Instala o sonnar
RUN dotnet tool install --global dotnet-sonarscanner --version 4.7.1
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /app
# Copiar os arquivos da solution para o container
COPY  . ./

ARG sonarLogin
# Start do scanner
RUN dotnet sonarscanner begin /k:"correia97_ImpostoRendaLB3" \
                              /o:"correia97" /d:sonar.host.url="https://sonarcloud.io" \
                              /d:sonar.login=$sonarLogin \
                              /d:sonar.cs.opencover.reportsPaths="/app/Tests/ImpostoRendaLB3.UnitTests/coverage.opencover.xml" \
                              /d:sonar.exclusions=**/*.js,**/*.css,**/obj/**,**/*.dll,**/*.html,**/*.cshtml
# Executa o restore
RUN dotnet restore
#Faz o build
RUN dotnet build
# Executa os teste
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
# Realiza a Analise
RUN dotnet sonarscanner end /d:sonar.login=$sonarLogin   
# Publica a Aplicação
RUN dotnet publish -c Release -o out
# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9 as API
WORKDIR /app
COPY --from=build-env /app/src/API/ImpostoRendaLB3.API/out .
ENTRYPOINT [ "dotnet","ImpostoRendaLB3.API.dll","--environment=docker"  ]
FROM correia97/netcoresdksonar:2.2 as build-env
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
RUN dotnet sonarscanner begin /k:"correia97_ImpostoRenda" \
                              /o:"correia97" /d:sonar.host.url="https://sonarcloud.io" \
                              /d:sonar.login=$sonarLogin \
                              /d:sonar.cs.opencover.reportsPaths="/app/Tests/ImpostoRenda.UnitTests/coverage.opencover.xml" \
                              /d:sonar.exclusions=**/*.js,**/*.css,**/obj/**,**/*.dll,**/*.html,**/*.cshtml,*-project.properties

#Faz o build
RUN dotnet build
# Executa os teste
RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --no-build
# Realiza a Analise
RUN dotnet sonarscanner end /d:sonar.login=$sonarLogin   
# Cobertura no CodCov
# download da ferramenta do Codecov
RUN curl -o codecov.sh https://codecov.io/bash
# Download das variaveis de ambinete
RUN curl -so codecovenv https://codecov.io/env
# Aceso a pasta
RUN chmod +x codecov.sh
# Aceso a pasta
RUN chmod +x codecovenv
# Definição das variáveis
ENV ci_env=./codecovenv
# Cobertura no CodCov
RUN ./codecov.sh -f "/app/Tests/ImpostoRenda.UnitTests/coverage.opencover.xml" -t $codecovToken
# Publica a Aplicação
RUN dotnet publish -c Release -o out
# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9 as API
# Define a pasta onde vai estar os arquivos
WORKDIR /app
# Copia os arquivos publicados do container de build para o container final
COPY --from=build-env /app/src/API/ImpostoRenda.API/out .
# Variavél de ambiente que define onde a aplicação está rodando
ENV ASPNETCORE_ENVIRONMENT=docker
# Define qual o executavel do container
ENTRYPOINT [ "dotnet","ImpostoRenda.API.dll"  ]

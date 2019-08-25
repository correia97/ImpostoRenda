FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.6-alpine3.9
WORKDIR /app
COPY /ImpostoRendaLB3.Web/out  .
ENTRYPOINT [ "dotnet","ImpostoRendaLB3.Web.dll","--environment=docker" ]

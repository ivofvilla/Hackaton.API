#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /app
#
#COPY ./Hackaton.Models/Hackaton.Models.csproj ./Hackaton.Models/
#COPY ./Hackaton.Api.Test/Hackaton.Api.Test.csproj ./Hackaton.Api.Test/
#COPY ./Hackaton.Worker/Hackaton.Worker.csproj ./Hackaton.Worker/
#COPY ./Hackaton.Api/Hackaton.Api.csproj ./Hackaton.Api/
#
## Restaure os pacotes para cada projeto
#WORKDIR /app/Hackaton.Models
#RUN dotnet restore Hackaton.Models.csproj
#
#WORKDIR /app/Hackaton.Api.Test
#RUN dotnet restore Hackaton.Api.Test.csproj
#
#WORKDIR /app/Hackaton.Worker
#RUN dotnet restore Hackaton.Worker.csproj
#
#WORKDIR /app/Hackaton.Api
#RUN dotnet restore Hackaton.Api.csproj
#
## Copie todos os arquivos de código e pastas necessárias
#COPY . .
#
## Defina o diretório de trabalho para o projeto principal
#WORKDIR /app/Hackaton.Api
#
## Compile o projeto
#RUN dotnet build Hackaton.Models/Hackaton.Models.csproj -c Release -o /build
#RUN dotnet build Hackaton.Api.Test/Hackaton.Api.Test.csproj -c Release -o /build
#RUN dotnet build Hackaton.Worker/Hackaton.Worker.csproj -c Release -o /build
#RUN dotnet build Hackaton.Api/Hackaton.Api.csproj -c Release -o /build
#
#RUN dotnet publish Hackaton.Api/Hackaton.Api.csproj -c Release -o /publish
## Use uma imagem mais leve para rodar a aplicação
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# EXPOSE 8080
# WORKDIR /publish
# COPY --from=build /publish .
# ENTRYPOINT ["dotnet", "Hackaton.Api.dll"]
#
#
##FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0-jammy-chiseled-composite
##EXPOSE 8080
##WORKDIR /app
##COPY --from=build /app .
##ENTRYPOINT ["dotnet", "Hackaton.Api.dll"]
#
##docker build -t roda-api .
##docker container run -p 8080:80 roda-api

##nova versao

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./Hackaton.Models/Hackaton.Models.csproj ./Hackaton.Models/
COPY ./Hackaton.Api.Test/Hackaton.Api.Test.csproj ./Hackaton.Api.Test/
COPY ./Hackaton.Worker/Hackaton.Worker.csproj ./Hackaton.Worker/
COPY ./Hackaton.Api/Hackaton.Api.csproj ./Hackaton.Api/

# Restaure os pacotes para cada projeto
WORKDIR /app/Hackaton.Models
RUN dotnet restore Hackaton.Models.csproj

WORKDIR /app/Hackaton.Api.Test
RUN dotnet restore Hackaton.Api.Test.csproj

WORKDIR /app/Hackaton.Worker
RUN dotnet restore Hackaton.Worker.csproj

WORKDIR /app/Hackaton.Api
RUN dotnet restore Hackaton.Api.csproj

# Copie todos os arquivos de código e pastas necessárias
COPY . .

# Defina o diretório de trabalho para o projeto principal
WORKDIR /app/Hackaton.Api

# Compile o projeto
RUN dotnet build Hackaton.Models/Hackaton.Models.csproj -c Release -o /build
RUN dotnet build Hackaton.Api.Test/Hackaton.Api.Test.csproj -c Release -o /build
RUN dotnet build Hackaton.Worker/Hackaton.Worker.csproj -c Release -o /build
RUN dotnet build Hackaton.Api/Hackaton.Api.csproj -c Release -o /build

RUN dotnet publish Hackaton.Api/Hackaton.Api.csproj -c Release -o /publish

# Use uma imagem mais leve para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
EXPOSE 80	
WORKDIR /publish
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Hackaton.Api.dll"]
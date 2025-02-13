# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de solução e de projetos para restaurar as dependências
COPY *.sln ./
COPY DataPersistenceService/DataPersistenceService.csproj DataPersistenceService/
COPY CreateContactService/CreateContactService.csproj CreateContactService/
COPY DeleteContactService/DeleteContactService.csproj DeleteContactService/
COPY GetContactService/GetContactService.csproj GetContactService/
COPY UpdateContactService/UpdateContactService.csproj UpdateContactService/
COPY TechChallenge.Application/TechChallenge.Application.csproj TechChallenge.Application/
COPY TechChallenge.Domain/TechChallenge.Domain.csproj TechChallenge.Domain/
COPY TechChallenge.Infrastructure/TechChallenge.Infrastructure.csproj TechChallenge.Infrastructure/

# Restaura as dependências do projeto DataPersistenceService
RUN dotnet restore DataPersistenceService/DataPersistenceService.csproj

# Copia o restante dos arquivos
COPY . .

# Exclui os arquivos de configuração duplicados antes do publish
RUN rm -f */appsettings.Development.json
RUN rm -f */appsettings.json

# Define o diretório de trabalho para o microsserviço DataPersistenceService
WORKDIR /src/DataPersistenceService

# Compila a aplicação especificando o caminho completo do projeto
RUN dotnet publish DataPersistenceService.csproj -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o build gerado para o contêiner final
COPY --from=build /app/out ./

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "DataPersistenceService.dll"]

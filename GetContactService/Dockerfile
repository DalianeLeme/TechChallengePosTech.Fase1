# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY ["GetContactService/GetContactService.csproj", "GetContactService/"]
COPY ["TechChallenge.Application/TechChallenge.Application.csproj", "TechChallenge.Application/"]
COPY ["TechChallenge.Domain/TechChallenge.Domain.csproj", "TechChallenge.Domain/"]
COPY ["TechChallenge.Infrastructure/TechChallenge.Infrastructure.csproj", "TechChallenge.Infrastructure/"]
RUN dotnet restore "GetContactService/GetContactService.csproj"

# Copia todo o código-fonte para o contêiner
COPY . .

# Define o diretório de trabalho para a compilação
WORKDIR "/src/GetContactService"

# Compila a aplicação
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia o build gerado para o contêiner final
COPY --from=build /app/out .

# Variável de ambiente opcional para configurações
ENV ASPNETCORE_ENVIRONMENT=Production

# Exposição da porta do microsserviço
EXPOSE 5000

# Executa o microsserviço
ENTRYPOINT ["dotnet", "GetContactService.dll"]

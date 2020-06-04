FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8080:80
EXPOSE 44336:443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Mutants.Api/Mutants.Api.csproj", "Mutants.Api/"]
RUN dotnet restore "Mutants.Api/Mutants.Api.csproj"
COPY . .
WORKDIR "/src/Mutants.Api"
RUN dotnet build "Mutants.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mutants.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mutants.Api.dll"]

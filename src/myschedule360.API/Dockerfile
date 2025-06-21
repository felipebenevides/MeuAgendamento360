FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/myschedule360.API/myschedule360.API.csproj", "src/myschedule360.API/"]
COPY ["src/myschedule360.Application/myschedule360.Application.csproj", "src/myschedule360.Application/"]
COPY ["src/myschedule360.Domain/myschedule360.Domain.csproj", "src/myschedule360.Domain/"]
COPY ["src/myschedule360.Infrastructure/myschedule360.Infrastructure.csproj", "src/myschedule360.Infrastructure/"]
RUN dotnet restore "src/myschedule360.API/myschedule360.API.csproj"
COPY . .
WORKDIR "/src/src/myschedule360.API"
RUN dotnet build "myschedule360.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "myschedule360.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myschedule360.API.dll"]
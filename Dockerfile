FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base
WORKDIR /app
COPY ["KAS.ECOS/KAS.ECOS.API.csproj", "KAS.ECOS/"]
COPY ["KAS.ECOS.Entity.DB/KAS.Entity.DB.ECOS.csproj", "KAS.ECOS.Entity.DB/"]
COPY ["KAS.ECOS.MIDDLEWARE/KAS.ECOS.MIDDLEWARE.csproj", "KAS.ECOS.MIDDLEWARE/"]
COPY ["KAS.ECOS.SERVICE/KAS.ECOS.SERVICE.csproj", "KAS.ECOS.SERVICE/"]
RUN dotnet restore "KAS.ECOS/KAS.ECOS.API.csproj"

COPY . .
RUN dotnet publish "/app/KAS.ECOS/KAS.ECOS.API.csproj" --runtime linux-musl-x64 -c Release -o publish -p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine
WORKDIR /app
COPY --from=base /app/publish .

EXPOSE 80
ENTRYPOINT ["./KAS.ECOS.API"]
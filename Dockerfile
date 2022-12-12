#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KAS.ECOS/KAS.ECOS.API.csproj", "KAS.ECOS/"]
COPY ["KAS.ECOS.Entity.DB/KAS.Entity.DB.ECOS.csproj", "KAS.ECOS.Entity.DB/"]
COPY ["KAS.ECOS.MIDDLEWARE/KAS.ECOS.MIDDLEWARE.csproj", "KAS.ECOS.MIDDLEWARE/"]
RUN dotnet restore "KAS.ECOS/KAS.ECOS.API.csproj"
COPY . .
WORKDIR "/src/KAS.ECOS"
RUN dotnet build "KAS.ECOS.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "KAS.ECOS.API.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "KAS.ECOS.API.dll" ]
# Multi-stage Dockerfile for ShoeShop (build with SDK, run with ASP.NET runtime)

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy solution and project files
COPY ["ShoeShop.sln", "./"]
COPY ["ShoeShop.WebApi/ShoeShop.WebApi.csproj", "ShoeShop.WebApi/"]
COPY ["ShoeShop.DataContext/ShoeShop.DataContext.csproj", "ShoeShop.DataContext/"]
COPY ["ShoeShop.Entities/ShoeShop.Entities.csproj", "ShoeShop.Entities/"]
COPY ["ShoeShop.Dto/ShoeShop.Dto.csproj", "ShoeShop.Dto/"]

# restore
RUN dotnet restore "ShoeShop.sln"

# copy everything else and publish
COPY . .
WORKDIR /src/ShoeShop.WebApi
ARG CONFIGURATION=Release
RUN dotnet publish "ShoeShop.WebApi.csproj" -c $CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# listen on port 80 by default
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "ShoeShop.WebApi.dll"]

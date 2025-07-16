# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# .csproj dosyasını kopyala
COPY ["RenartApi/RenartApi.csproj", "RenartApi/"]

# Restore işlemi
RUN dotnet restore "RenartApi/RenartApi.csproj"

# Tüm dosyaları kopyala
COPY . .

# Projeyi publish et
WORKDIR "/src/RenartApi"
RUN dotnet publish "RenartApi.csproj" -c Release -o /app/publish

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "RenartApi.dll"]

# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Projeyi kopyala
COPY . .

# Bağımlılıkları restore et ve derle
RUN dotnet restore "RenartApi/RenartApi.csproj"
RUN dotnet publish "RenartApi/RenartApi.csproj" -c Release -o out

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Uygulamanın dış dünyaya açılacağı port
EXPOSE 80

# Başlangıç komutu
ENTRYPOINT ["dotnet", "RenartApi.dll"]

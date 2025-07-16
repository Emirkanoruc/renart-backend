# Temel ASP.NET Runtime Ortamı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 # Uygulamanızın varsayılan dinleme portu

---

# Uygulamanın Derlenmesi İçin SDK Ortamı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını ve bağımlılıkları kopyala ve geri yükle
# RenartApi.csproj dosyasının Dockerfile ile aynı dizinde olduğundan emin olun.
# Eğer RenartApi.csproj bir alt dizindeyse (örn: 'RenartApi/RenartApi.csproj'),
# burayı ona göre "RenartApi/RenartApi.csproj" olarak değiştirmelisiniz.
COPY ["RenartApi.csproj", "RenartApi.csproj"]
RUN dotnet restore "RenartApi.csproj"

# Kalan tüm proje dosyalarını kopyala
# Bu komut, kaynak kodunuzun tamamını (örneğin .cs dosyaları, controller'lar vb.) /src içine kopyalar.
COPY . .

# Uygulamayı yayınla (build et ve dağıtıma hazır hale getir)
# UseAppHost=false, Linux kapsayıcıları için genellikle önerilir.
RUN dotnet publish "RenartApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

---

# Son Üretim Ortamı (Küçük ve Hızlı)
FROM base AS final
WORKDIR /app
# Daha önceki 'build' aşamasında oluşturulan yayınlanmış çıktıları kopyala
COPY --from=build /app/publish .

# Uygulamanın başlangıç komutu
ENTRYPOINT ["dotnet", "RenartApi.dll"]

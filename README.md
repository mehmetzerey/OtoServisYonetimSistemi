# Oto Servis Yönetim Sistemi

Araç servis operasyonlarını yönetmek için geliştirilmiş kapsamlı bir .NET Core uygulaması.

## Proje Hakkında

Oto Servis Yönetim Sistemi, araç servis operasyonlarını yönetmek için Clean Architecture prensiplerine uygun olarak tasarlanmış modern bir .NET uygulamasıdır.

## Teknik Özellikler

- **.NET 7.0**
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **Swagger/OpenAPI**
- **JWT Authentication**

## Mimari Yapı

- **Domain Layer**: İş domaininin temel yapıtaşlarını içerir
- **Application Layer**: İş mantığı ve uygulama servisleri
- **Persistence Layer**: Veritabanı işlemleri ve veri erişimi
- **API Layer**: REST API endpoints ve dış dünya ile iletişim
- **Infrastructure Layer**: Harici servisler ve teknik altyapı

## Kurulum

```bash
# Repoyu klonlayın
git clone https://github.com/mehmetzerey/OtoServisYonetimSistemi.git

# Proje dizinine gidin
cd OtoServisYonetimSistemi

# Bağımlılıkları yükleyin
dotnet restore

# Projeyi derleyin
dotnet build

# API projesini çalıştırın
dotnet run --project src/OtoServisYonetim.API
```

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakınız.
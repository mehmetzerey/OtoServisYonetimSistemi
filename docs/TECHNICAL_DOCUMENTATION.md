# Oto Servis Yönetim Sistemi - Teknik Döküman

## 1. Proje Genel Bakış

Oto Servis Yönetim Sistemi, araç servis operasyonlarını yönetmek için geliştirilmiş kapsamlı bir .NET Core uygulamasıdır. Sistem Clean Architecture prensiplerine uygun olarak tasarlanmış ve modern .NET teknolojilerini kullanmaktadır.

## 2. Teknik Mimari

### 2.1. Katmanlı Mimari
- **Domain Layer**: İş domaininin temel yapıtaşlarını içerir
- **Application Layer**: İş mantığı ve uygulama servisleri
- **Persistence Layer**: Veritabanı işlemleri ve veri erişimi
- **API Layer**: REST API endpoints ve dış dünya ile iletişim
- **Infrastructure Layer**: Harici servisler ve teknik altyapı

### 2.2. Kullanılan Teknolojiler
- **.NET 7.0**
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **Swagger/OpenAPI**
- **JWT Authentication**

## 3. Domain Katmanı

### 3.1. Temel Entities
- **Customer**: Müşteri bilgileri
- **Vehicle**: Araç bilgileri
- **Mechanic**: Teknisyen bilgileri
- **RepairIssue**: Arıza/tamir kayıtları

### 3.2. Value Objects
- **Address**: Adres bilgileri (Sokak, İlçe, İl, Posta Kodu)
- **PhoneNumber**: Telefon numarası validasyonu ve formatlaması
- **VehicleIdentification**: Araç kimlik bilgileri (Şasi No, Motor No)
- **Money**: Para birimi ve tutarı

### 3.3. Enums
- **VehicleType**: Sedan, SUV, Hatchback, vb.
- **RepairStatus**: Beklemede, Başlandı, Tamamlandı, İptal
- **PaymentStatus**: Ödenmedi, Kısmi Ödeme, Tamamlandı
- **CustomerType**: Bireysel, Kurumsal

### 3.4. Domain Events
- **RepairCompleted**: Tamir tamamlandığında tetiklenir
- **PaymentReceived**: Ödeme alındığında tetiklenir

## 4. Application Katmanı

### 4.1. CQRS Pattern
- **Commands**: Veri değiştirme işlemleri
- **Queries**: Veri sorgulama işlemleri
- **Command/Query Handlers**: İş mantığı uygulaması

### 4.2. Servisler
- **CustomerService**: Müşteri yönetimi
- **VehicleService**: Araç yönetimi
- **RepairService**: Tamir süreçleri yönetimi
- **BillingService**: Fatura ve ödeme işlemleri

### 4.3. DTOs (Data Transfer Objects)
- **Request Models**: Gelen isteklerin modelleri
- **Response Models**: Dönüş yapıları
- **Validation Rules**: FluentValidation kuralları

### 4.4. Behaviors
- **ValidationBehavior**: İstek validasyonu
- **LoggingBehavior**: İşlem kayıtları
- **TransactionBehavior**: Transactional operasyonlar

## 5. Persistence Katmanı

### 5.1. Repository Pattern
- Generic Repository Base
- Entity özel repository'ler
- Unit of Work pattern

### 5.2. Veritabanı İşlemleri
- Entity Framework Core kullanımı
- Code-First yaklaşımı
- Migration yönetimi

### 5.3. Entity Configurations
- Fluent API ile entity yapılandırması
- İlişki tanımlamaları
- Veri kısıtlamaları (constraints)

### 5.4. Seed Data
- Başlangıç verileri
- Test verileri
- Referans verileri

## 6. API Katmanı

### 6.1. Controller Yapısı
- REST standartlarına uygun endpoints
- Versiyonlama desteği
- Swagger dokümantasyonu

### 6.2. Güvenlik
- JWT tabanlı kimlik doğrulama
- Role bazlı yetkilendirme
- HTTPS zorunluluğu

### 6.3. Middleware
- Exception handling middleware
- Request/response logging
- Performans izleme
- Correlation ID ekleme

### 6.4. API Filters
- Action filters
- Authorization filters
- Resource filters
- Exception filters

## 7. Kod Standartları

### 7.1. Naming Conventions
- PascalCase: Class, Method, Property
- camelCase: Değişkenler
- I prefix: Interface'ler

### 7.2. Dosya Organizasyonu
- Feature bazlı klasör yapısı
- Separation of Concerns prensibi
- Clean Architecture kuralları

### 7.3. Kod Kalitesi
- Unit testler
- Integration testler
- Code review standartları
- Static kod analizi araçları

### 7.4. Dokümantasyon
- XML Comments
- README dosyaları
- Swagger açıklamaları
- Kullanım örnekleri

## 8. Performans Optimizasyonları

### 8.1. Veritabanı
- İndeksleme stratejileri
- Lazy loading kontrolü
- N+1 query önleme

### 8.2. Uygulama
- Asenkron programlama
- Caching mekanizmaları
- Resource pooling
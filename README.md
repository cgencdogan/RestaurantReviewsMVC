# Restoran İnceleme
> Restoranlar hakkında inceleme yazabileceğiniz, diğer kullanıcıların yorumlarını okuyabileceğiniz bir ASP.NET MVC projesi.

## Özet
> Restoran İnceleme, kullanıcıların sistemde girilen ilçelere ve kategorilere göre yer alan restoranları inceleyebileceği bir ASP.NET MVC projesidir. Kullanıcılar, restoranlar hakkında adres ve telefon gibi bilgilere ulaşabilmenin yanında yorum yapabilir, puan verebilir, fotoğraf yükleyebilir, restoranları ilçesine, kategorisine veya puanına göre sıralayabilir. Yönetici rolüne sahip olan kullanıcılar ise yeni yönetim panelinden çeşitli istatistiklere ve grafiklere ulaşabilir. Restoran, kategori, servis gibi bilgileri ekleyip güncelleyebilir. Kullanıcıları ve yorumları yönetebilir. Projenin yönetim paneli için "SB Admin 2" isimli Bootstrap teması kullanılmıştır. Restoran sayfası ve ana sayfa ile bootstrap kullanılarak tasarlanmıştır.

## Çalıştırmadan Önce
- **Solution**'a sağ tıklayarak "**Restore Nuget Packages"** seçeneği ile projede kullanılan paketleri yükleyiniz.
- **Package Manager Console**'da Default Project olarak **RestaurantReviews.DAL** projesini seçiniz.
- **Package Manager Console**'da **"Update-Database"** komutunu girerek veri tabanını oluşturunuz.
- Deneme amacıyla veri eklemek isterseniz **RestaurantReviews.DAL>Seeds>SeedData.sql** dosyasını execute ediniz.
- **RestaurantReviews.WebUI** projesine sağ tıklayarak **"Set as Startup Project"** seçeneğine tıklayınız.
- Projeyi çalıştırdığınızda **"roslyn\csc.exe yolunun bir parçası bulunamadı."** hatası ile karşılaşırsanız, Package Manage Console'da **"Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r"** komutu ile paketi güncelleyiniz.

## Kullanıcı Bilgileri
**Kullanıcı Adı:** admin
**Şifre :** qwe123

## Kullanılan Teknolojiler
- ASP.NET MVC 5 - version 5.2.7
- Entity Framework Code First - version 6.2.0
- AspNet IdentityCore - version 2.2.2
- Microsoft Owin - version 4.0.1
- Newtonsoft.Json - version 12.0.2
- jQuery - version 3.4.1
- Bootstrap - version 3.4.1
- Toastr - version 2.1.1
- SweetAlert2- version 8.13.2

## Ekran Görüntüleri
![Example screenshot](./img/screenshot.png)
<!-- If you have screenshots you'd like to share, include them here. -->

## İletişim
[LinkedIn](https://www.linkedin.com/in/caner-gencdogan/)

[Gmail](mailto:gencdogancaner@gmail.com)
